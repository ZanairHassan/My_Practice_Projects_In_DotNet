using BLL.Interfaces;
using DAL.DBContext;
using DAL.Models;
using DAL.ViewModels;
using DAL.ViewModels.BulkViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Utilities;
namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly AuditAppDBContext _auditDBContext;
        private readonly ITokenService _tokenService;
        private const int MaxFailedAttempts = 3;
        private readonly AsymmetricCryptographyUtility _asymmetricCryptograpyUtility;
        private readonly IPasswordHasher<User> _passwordHasher;
        public UserService(AuditAppDBContext auditDBContext,
            ITokenService tokenService,
            AsymmetricCryptographyUtility asymmetricCryptographyUtility,
            IPasswordHasher<User> passwordHasher)
        {
            _auditDBContext = auditDBContext;
            _asymmetricCryptograpyUtility = asymmetricCryptographyUtility;
            _passwordHasher = passwordHasher;
            _tokenService= tokenService;
        }

        public async Task<User> CreateUser(UserVM userVM)
        {
            User user = new User();
            user.CreatedDate = DateTime.Now;
            user.UserName = userVM.UserName;
            user.Password = userVM.Password;
            user.Email = userVM.Email;
            user.FirstName = userVM.FirstName;
            user.LastName = userVM.LastName;
            user.IsActive = true;
            await _auditDBContext.Users.AddAsync(user);
            await _auditDBContext.SaveChangesAsync();
            return user;
        }

        public async Task<UserVM> RegisterUser(string usertype, UserVM user)
        {
            User usermodel = new User();

            var existingUser = await _auditDBContext.Users.FirstOrDefaultAsync(u => u.UserName == user.UserName || u.Email == user.Email);
            if (existingUser != null)
            {
                LoggingUtlity.LogText("UserName or Email Is already Exist", null);
                user.SuccessMessage = string.Empty;
                user.ErrorMessage = "User with this username or email address already exist";
                return user;
            }

            var userTypeExisting = await _auditDBContext.UsersTypes.FirstOrDefaultAsync(x => x.UserType == usertype);

            if (existingUser == null && userTypeExisting != null)
            {
                usermodel.UserName = user.UserName;
                usermodel.Email = user.Email;
                usermodel.Password = _passwordHasher.HashPassword(null, user.Password);
                usermodel.FirstName = user.FirstName;
                usermodel.LastName = user.LastName;
                usermodel.PhoneNo = user.PhoneNo;
                usermodel.CreatedBy = user.CreatedBy;
                usermodel.CreatedDate = user.CreatedDate;
                usermodel.DeletedDate = user.DeletedDate;
                usermodel.DeletedBy = user.DeletedBy;
                usermodel.UserTypesID = user.UserTypeID;
                usermodel.IsActive = true;
                usermodel.IsDeleted = false;
                usermodel.ModifiedBy = user.ModifiedBy;
                usermodel.ModifiedDate = user.ModifiedDate;
                user.ErrorMessage = string.Empty;
                user.SuccessMessage = "User is created successfully";
                usermodel.UserTypesID = userTypeExisting.UserTypesID;
                await _auditDBContext.Users.AddAsync(usermodel);
                await _auditDBContext.SaveChangesAsync();

            }
            return user;
            //return Ok("User registered successfully");
        }

        public async Task<User> BlockUser(int userID)
        {
            var user = await _auditDBContext.Users.FirstOrDefaultAsync(x => x.UserID == userID);
            if (user != null)
            {
                user.IsBlocked = true;
                user.FailedLoginAttempts = 0; // Reset failed attempts count
                _auditDBContext.Users.Update(user);
                await _auditDBContext.SaveChangesAsync();
            }
            return user;
        }

        public async Task<User> Login(LoginVM loginVm)
        {
            var loggedInUser = await _auditDBContext.Users.FirstOrDefaultAsync(x => x.UserName == loginVm.UserName && x.IsDeleted == false && x.IsActive == true && x.IsBlocked == false);

            if (loggedInUser != null)
            {
                var decryptedPassword = _passwordHasher.VerifyHashedPassword(null, loggedInUser.Password, loginVm.Password);
                if (decryptedPassword == PasswordVerificationResult.Success)
                {
                    loggedInUser.FailedLoginAttempts = 0;
                    _auditDBContext.Users.Update(loggedInUser);

                    var existingToken = await _tokenService.GetTokenByUserId(loggedInUser.UserID);
                    if (existingToken != null)
                    {
                        await _tokenService.ExpireTokensByUserId(loggedInUser.UserID);
                    }

                    await _auditDBContext.SaveChangesAsync();
                    return loggedInUser;
                }
                else
                {
                    loggedInUser.FailedLoginAttempts++;
                    _auditDBContext.Users.Update(loggedInUser);
                    await _auditDBContext.SaveChangesAsync();


                    if (loggedInUser.FailedLoginAttempts >= MaxFailedAttempts)
                    {
                        await BlockUser(loggedInUser.UserID);
                    }
                }
            }

            return null;
        }

        public async Task<ChangePasswordVM> ChangePassword(ChangePasswordVM changePasswordVM)
        {
            var user = await _auditDBContext.Users.FirstOrDefaultAsync(x => x.UserID == changePasswordVM.UserID);
            if (user != null)
            {
                user.ModifiedDate = DateTime.Now;
                user.Password = _passwordHasher.HashPassword(null, changePasswordVM.NewPassword);
                _auditDBContext.Users.Update(user);
                await _auditDBContext.SaveChangesAsync();
                changePasswordVM.SuccessMessage = "User is updated successfully";
                changePasswordVM.ErrorMessage = string.Empty;
            }
            else
            {
                changePasswordVM.ErrorMessage = "User is not found to update";
                changePasswordVM.SuccessMessage = string.Empty;
            }
            return changePasswordVM;
        }


        public async Task<User> GuestUser(GuestUserVM guestUserVM)
        {
            try
            {
                string guestEmail = guestUserVM.GuestEmail;

                User guestUser = null;

                var existingGuest = await _auditDBContext.Users.FirstOrDefaultAsync(u => u.Email == guestEmail);
                if (existingGuest == null)
                {
                    guestUser = new User
                    {
                        Email = guestUserVM.GuestEmail,
                        UserName = guestEmail,

                        Password = _passwordHasher.HashPassword(null, Guid.NewGuid().ToString()), //random password
                        FirstName = guestUserVM.GuestFirstName,
                        LastName = guestUserVM.GuestLastName,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedDate = DateTime.Now
                    };

                    await _auditDBContext.Users.AddAsync(guestUser);
                    await _auditDBContext.SaveChangesAsync();
                }
                else
                {
                    guestUser = existingGuest;
                }
                return guestUser;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the guest user", ex);
            }
        }

        public async Task<User> GetUserByToken(string jwtToken)
        {
            var token = await _auditDBContext.Tokens
                .FirstOrDefaultAsync(x => x.JwtToken == jwtToken && x.IsExpired == false && x.IsDeleted == false);

            if (token != null)
            {
                var user = await _auditDBContext.Users
                    .FirstOrDefaultAsync(u => u.UserID == token.UserID && u.IsDeleted == false && u.IsActive == true);

                return user;
            }

            return null;
        }


        public async Task<User> DeleteUser(int userID)
        {
            var user = await _auditDBContext.Users.FirstOrDefaultAsync(x => x.UserID == userID);
            if (user != null)
            {
                user.IsDeleted = true;
                await _auditDBContext.SaveChangesAsync();
            }
            return user;
        }

        public async Task<List<User>> GetAllUser()
        {
            return await _auditDBContext.Users.AsNoTracking().ToListAsync();
        }

        public async Task<User> GetUser(int userID)
        {
            return await _auditDBContext.Users.FirstOrDefaultAsync(x => x.UserID == userID);
        }

        public async Task<List<User>> GetAllBlockUsers()
        {
            return await _auditDBContext.Users
                .Where(x => x.IsBlocked == true)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var emailget = Convert.ToBase64String(_asymmetricCryptograpyUtility.EncryptData(email));
            var user= await _auditDBContext.Users.FirstOrDefaultAsync(x => x.Email == emailget);
            if (user != null)
            {
                return user;
            }
            return user;
        }

        public Task<User> GetUserByName(string name)
        {
            return _auditDBContext.Users.FirstOrDefaultAsync(x=> x.UserName == name);
        }

        public async Task<User> UpdateUser(int userID, UserVM userVM)
        {
            var user = await _auditDBContext.Users.FirstOrDefaultAsync(x => x.UserID == userID);
            if (user != null)
            {
                user.ModifiedDate = DateTime.Now;
                user.ModifiedBy = userVM.UsersID;
                user.UserName = userVM.UserName;
                user.Password = userVM.Password;
                user.Email = userVM.Email;
                user.FirstName = userVM.FirstName;
                user.LastName = userVM.LastName;
                user.IsActive = true;
                await _auditDBContext.SaveChangesAsync();
            }
            return user;
        }

        public async Task<List<User>> BulkActive(List<int> userId, bool isActive)
        {
            var users=await _auditDBContext.Users
                .Where(x=> userId
                .Contains(x.UserID) && (x.IsDeleted==null || x.IsDeleted==false))
                .AsNoTracking()
                .ToListAsync();
            foreach (var user in users)
            {
                if (user != null)
                {
                    user.IsActive = isActive;
                }
                _auditDBContext.Users.Update(user);
                await _auditDBContext.SaveChangesAsync();
            }
                _auditDBContext.Users.UpdateRange(users);
                await _auditDBContext.SaveChangesAsync();
                return users;
            
        }

        public async Task<List<User>> BulkDeleteUser(List<int> usersId)
        {
            var users = await _auditDBContext.Users
                .Where(f => usersId
                .Contains(f.UserID) && (f.IsDeleted == null || f.IsDeleted == false))
                .AsNoTracking()
                .ToListAsync();
            foreach (var user in users)
            {
                user.IsDeleted = true;
                user.IsActive = false;
            }

            _auditDBContext.Users.UpdateRange(users);
            await _auditDBContext.SaveChangesAsync();
            return users;
        }


    }
}
