using BLL.Interfaces;
using DAL.DBContext;
using DAL.Models;
using DAL.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class UserTypesService: IUserTypesService
    {
        private readonly AuditAppDBContext _auditDBContext;
        public UserTypesService(AuditAppDBContext auditDBContext)
        {
            _auditDBContext = auditDBContext;
        }
        public async Task<UserTypesVM> CreateUserTypes(UserTypesVM usertypesVM)
        {
            if (usertypesVM == null)
            {
                throw new ArgumentNullException(nameof(usertypesVM), "usertypesVM cannot be null");
            }
            ValidateUserTypeDetailsVM(usertypesVM);

            if (await IsUserTypeDuplicate(usertypesVM.UserType))
            {
                throw new ArgumentException("UserType  already exists ", nameof(usertypesVM.UserType));
            }

            UserTypes userTypes = new UserTypes();
            userTypes.UserType = usertypesVM.UserType;
            await _auditDBContext.UsersTypes.AddAsync(userTypes);
            await _auditDBContext.SaveChangesAsync();
            usertypesVM.ErrorMessage = string.Empty;
            usertypesVM.SuccessMessage = "UserType is created successfully";
            return usertypesVM;
        }

        public async Task<UserTypes> DeleteUserTypes(int usertypesID)
        {
            var usertypes = await _auditDBContext.UsersTypes
                .FirstOrDefaultAsync(x => x.UserTypesID == usertypesID);
            if (usertypes != null)
            {
                usertypes.IsDeleted = true;
                await _auditDBContext.SaveChangesAsync();
            }
            return usertypes;
        }

        public async Task<List<UserTypes>> GetAllUserTypes()
        {
            return await _auditDBContext.UsersTypes.AsNoTracking().ToListAsync();
        }

        public async Task<UserTypes> GetUserTypes(int usertypesID)
        {
            return await _auditDBContext.UsersTypes
                .FirstOrDefaultAsync(x => x.UserTypesID == usertypesID);
        }

        public async Task<UserTypesVM> UpdateUserTypes(int usertypesID, UserTypesVM usertypesVM)
        {
            var usertypes = await _auditDBContext.UsersTypes
                .FirstOrDefaultAsync(x => x.UserTypesID == usertypesID);
           if(usertypes is null)
            {
                return null;
            }
            usertypes.UserType = usertypesVM.UserType;
            _auditDBContext.UsersTypes.Update(usertypes);
            await _auditDBContext.SaveChangesAsync();
            usertypesVM.ErrorMessage = string.Empty;
            usertypesVM.SuccessMessage = "usertype is created successfully";
            return usertypesVM;
        }

        public async Task<List<UserTypes>> BulkDelete(List<int> userTypeId)
        {
            var userTypes = await _auditDBContext.UsersTypes
                .Where(f => userTypeId
                .Contains(f.UserTypesID) && (f.IsDeleted == null || f.IsDeleted == false))
                .AsNoTracking()
                .ToListAsync();
            foreach (var usertype in userTypes)
            {
                usertype.IsDeleted = true;
            }

            _auditDBContext.UsersTypes.UpdateRange(userTypes);
            await _auditDBContext.SaveChangesAsync();
            return userTypes;
        }

        private void ValidateUserTypeDetailsVM(UserTypesVM userTypeVM)
        {
            if (string.IsNullOrWhiteSpace(userTypeVM.UserType))
            {
                throw new ArgumentException("usertype type cannot be empty or null", nameof(UserTypesVM.UserType));
            }
        }

        private async Task<bool> IsUserTypeDuplicate(string userType)
        {
            return await _auditDBContext.UsersTypes.AnyAsync(x => x.UserType == userType);
        }

    }



}
