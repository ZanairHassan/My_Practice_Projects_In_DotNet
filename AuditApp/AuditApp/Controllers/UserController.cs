using BLL.Interfaces;
using BLL.Services;
using DAL.DBContext;
using DAL.Models;
using DAL.ViewModels;
using DAL.ViewModels.BulkViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;
using System.Net.WebSockets;
using System.Text;
using Utilities;

namespace AuditApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly string _storagePath = Path.Combine(Directory.GetCurrentDirectory(), "ProfilePictures");
        private readonly SymmetricCryptographyUtility _symmetricCryptograpyUtility;
        private readonly AsymmetricCryptographyUtility _asymmetricCryptograpyUtility;
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;
        private readonly IUserTypesService _userTypesService;
        private readonly IRequestService _requestService;
        // private readonly  LoggingUtlity _logger;

        public UserController(
            AsymmetricCryptographyUtility asymmetricCryptographyUtility,
            SymmetricCryptographyUtility symmetricCryptograpyUtility,
            ITokenService tokenService,
        IUserTypesService userTypesService,
        AsymmetricCryptographyUtility asymmetricCryptograpyUtility,
            IUserService userService,
            IConfiguration configuration,
            IServiceProvider serviceProvider,
            IRequestService requestService
            //LoggingUtlity logger
            )
        {
            _symmetricCryptograpyUtility = symmetricCryptograpyUtility;
            _tokenService = tokenService;
            _asymmetricCryptograpyUtility = asymmetricCryptograpyUtility;
            _userService = userService;
            _configuration = configuration;
            _serviceProvider = serviceProvider;
            _userTypesService = userTypesService;
            _requestService = requestService;
            //  _logger = logger;
        }



        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody]UserVM user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(BLL.Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }
            try
            {
                LoggingUtlity.LogText("Registering User", _configuration);
                var userType = await _userTypesService.GetUserTypes(user.UserTypeID);

                var user1 = await _userService.RegisterUser(userType.UserType, user);
                LoggingUtlity.LogText("Registered User ", _configuration);
                return Ok(user1);
            }
            catch (Exception ex)
            {
                LoggingUtlity.ExLog(ex.ToString(), _serviceProvider);
                throw;
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginVM loginVm)
        {
            LoggingUtlity.LogText("Login IsCalled", _configuration);

            if (!ModelState.IsValid)
            {
                return BadRequest(BLL.Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }
            try
            {
                var genToken = new Token();
                var user = await _userService.Login(loginVm);
                var userType = await _userTypesService.GetUserTypes(user.UserTypesID);
                if (userType == null)
                {
                    return NotFound("User Not Found Please Enter Valid User");
                }

                 await _requestService.RemoveUserRequest(user.UserID);
                if (user != null)
                {
                    var tokon = TokenUtility.GenerateToken(user.Email);
                    TokenVM tokenVM = new TokenVM();
                    tokenVM.IsExpired = false;
                    tokenVM.UserID = user.UserID;
                    tokenVM.JwtToken = tokon;
                    tokenVM.IsDeleted = false;
                    genToken = await _tokenService.CreateToken(tokenVM);
                }
                else
                {
                    return BadRequest("Invalid Username or password");
                }
                return Ok(genToken);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }


        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("ChangePassword")]
        [HttpPut]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordVM changePasswordVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(BLL.Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }

            try
            {
                LoggingUtlity.LogText("getting the user of which the password is being  change", _configuration);

                LoggingUtlity.LogText("modifing password", _configuration);
                var result = await _userService.ChangePassword(changePasswordVM);

                if (!string.IsNullOrEmpty(result.ErrorMessage))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, result.ErrorMessage);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Handle exception properly
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }


        [Route("GuestUser")]
        [HttpPost]
        public async Task<IActionResult> GuestUser(GuestUserVM guestUserVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(BLL.Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }

            try
            {
                var user = await _userService.GuestUser(guestUserVM);
                if (user == null)
                {
                    return BadRequest("Unable to create guest user.");
                }

                var tokenString = TokenUtility.GenerateToken(user.Email);

                TokenVM tokenVM = new TokenVM
                {
                    IsExpired = false,
                    UserID = user.UserID,
                    JwtToken = tokenString,
                    IsDeleted = false
                };

                var token = await _tokenService.CreateToken(tokenVM);

                if (token == null)
                {
                    return BadRequest("Unable to create a token for the guest user.");
                }

                return Ok(new { Token = token, User = user });
            }
            catch (Exception ex)
            {
                LoggingUtlity.LogText(ex.ToString(), _configuration);
                LoggingUtlity.ExLog(ex.Message, _serviceProvider);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the guest user and token.");
            }
        }

        [Route("GetUserByToken/{jwtToken}")]
        [HttpGet]
        public async Task<IActionResult> GetUserByToken(string jwtToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(BLL.Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }

            var user = await _userService.GetUserByToken(jwtToken);
            if (user == null)
            {
                return NotFound("User not found or token is invalid.");
            }

            return Ok(user);
        }


        //******************************* Forget Password ****************
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(BLL.Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }

            var user = await _userService.GetUserByEmail(model.Email);
            if (user == null)
            {
                LoggingUtlity.LogText("User with provided email not found.", _configuration);
                return BadRequest("User with provided email not found.");
            }
            try
            {
                string temporaryPassword = GenerateTemporaryPassword();
                UserVM userVM = new UserVM();
                userVM.UserName = user.UserName;
                userVM.UsersID = user.UserID;
                userVM.Email = user.Email;
                userVM.PhoneNo = user.PhoneNo;
                userVM.FirstName = user.FirstName;
                userVM.LastName = user.LastName;
                userVM.Password = temporaryPassword;
                await _userService.UpdateUser(user.UserID, userVM);
                EmailSentForForgetPassword.SendEmailForPassword(user.Email, temporaryPassword);
                var body = $"Your temporary password is: {temporaryPassword}";
                SMSTypeRequest.SendSmS(user.PhoneNo, temporaryPassword, body);
                return Ok("Password reset instructions sent successfully.");
            }
            catch (Exception ex)
            {
                LoggingUtlity.LogText (ex.ToString(), _configuration);
                LoggingUtlity.ExLog(ex.Message, _serviceProvider);
                throw;
            }

        }

        [HttpGet("GetUser/{id}")]
        public async Task<IActionResult> GetTheUser(int id)
        {
            var findUser=await _userService.GetUser(id);
            if(findUser == null)
            {
                LoggingUtlity.LogText("user didnot exist",_configuration);
                return NotFound();
            }
            try
            {
                LoggingUtlity.LogText("user has successfully found", _configuration);
                return Ok(findUser);
            }
            catch (Exception ex)
            {
                LoggingUtlity.ExLog(ex.Message,_serviceProvider);
                throw;
            }
        }
        private string GenerateTemporaryPassword()
        {
            return Guid.NewGuid().ToString();
        }

        [Route("GetAllBlockedUser")]
        [HttpGet]
        public async Task<IActionResult> GetAllBlockedUsers()
        {
            var allBlockUser = await _userService.GetAllBlockUsers();
            return Ok(allBlockUser);

        }

        private ObjectResult ReturnException(Exception exception, IConfiguration configuration)
        {
            LoggingUtlity.LogText(exception.ToString(), configuration);
            return StatusCode(StatusCodes.Status500InternalServerError, exception.ToString());
        }

        [Route("BulkDelete")]
        [HttpPost]
        public async Task<IActionResult> BulkDelete([FromBody] UserBulkVM model)
        {
            LoggingUtlity.LogText("BulkDelete IsCalled", _configuration);

            if (!ModelState.IsValid)
            {
                return BadRequest(BLL.Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }
            LoggingUtlity.LogText("BulkDeleting Users", _configuration);

            var deletedUsers = await _userService.BulkDeleteUser(model.userIds);
            LoggingUtlity.LogText("BulkDeleted Users", _configuration);

            return Ok(deletedUsers);
        }

        [Route("BulkActive")]
        [HttpPut]
        public async Task<IActionResult> BulkActive([FromBody] UserBulkVM model)
        {
            LoggingUtlity.LogText("BulkActive IsCalled", _configuration);

            if (!ModelState.IsValid)
            {
                return BadRequest(BLL.Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }
            LoggingUtlity.LogText("BulkActivating Users", _configuration);

            var activeUsers = await _userService.BulkActive(model.userIds, model.IsActive);
            LoggingUtlity.LogText("BulkActivated Users", _configuration);

            return Ok(activeUsers);
        }

    }
}
