
using BLL;
using BLL.Interfaces;
using BLL.Services;
using DAL.Models;
using DAL.ViewModels;
using DAL.ViewModels.BulkViewModels;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace AuditApp.Controllers
{
    [ServiceFilter(typeof(AuthenticateURL))]
    [Route("api/[controller]")]
    [ApiController]
    public class UserTypeController : ControllerBase
    {

        private readonly IUserTypesService _userTypesService;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;
        private readonly IRequestService _requestService;
        private readonly ITokenService _tokenService;

        public UserTypeController(ITokenService tokenService,
            IRequestService requestService,
            IUserTypesService userTypesService,
            IConfiguration configuration,
            IServiceProvider serviceProvider)
        {
            _requestService = requestService;
            _userTypesService = userTypesService;
            _configuration = configuration;
            _serviceProvider = serviceProvider;
            _tokenService = tokenService;
        }

        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("CreateUserType")]
        [HttpPost]
        public async Task<IActionResult> CreateUserType([FromBody] UserTypesVM userTypesVM)
        {
            LoggingUtlity.LogText("CreateUserTypeetails IsCalled", _configuration);
            if (!ModelState.IsValid)
            {
                return BadRequest(Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }
            try
            {
                LoggingUtlity.LogText("Creating UserType", _configuration);
                var userType = await _userTypesService.CreateUserTypes(userTypesVM);
                if (!string.IsNullOrEmpty(userType.SuccessMessage))
                {
                    LoggingUtlity.LogText(userType.SuccessMessage, _configuration);
                }
                else
                {
                    LoggingUtlity.LogText(userType.ErrorMessage, _configuration);
                }
                return Ok(userType);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }

        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("DeleteUserType/{usertypeID}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUserType(int usertypeID)
        {
            LoggingUtlity.LogText("Deleting UserType IsCalled", _configuration);
            try
            {
                var deleteUserType = await _userTypesService.DeleteUserTypes(usertypeID);
                LoggingUtlity.LogText("UserType Deleted", _configuration);
                return Ok(deleteUserType);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }


        [Route("GetAllUserType")]
        [HttpGet]
        public async Task<IActionResult> GetAllUserType()
        {
            try
            {
                LoggingUtlity.LogText("Fetching All UserType Details", _configuration);

                var allUserType = await _userTypesService.GetAllUserTypes();
                LoggingUtlity.LogText("GetUserType: Retrieved all UserType", _configuration);
                return Ok(allUserType);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }


        [Route("GetUserType/{id}")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserType(int id)
        {
            try
            {
                var getUserType = await _userTypesService.GetUserTypes(id);

                LoggingUtlity.LogText($"GetUserType: Retrieved user type with ID", _configuration);
                return Ok(getUserType);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }


        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("UpdateUserType/{usertypeID}")]
        [HttpPost]
        public async Task<IActionResult> UpdateUserType(int usertypeID, UserTypesVM userTypesVM)
        {
            LoggingUtlity.LogText("UpdateUserType IsCalled", _configuration);
            if (!ModelState.IsValid)
            {
                return BadRequest(Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }
            try
            {
                LoggingUtlity.LogText("Updating userTupe", _configuration);

                var updateUserType = await _userTypesService.UpdateUserTypes(usertypeID, userTypesVM);
                if (!string.IsNullOrEmpty(updateUserType.SuccessMessage))
                {
                    LoggingUtlity.LogText(updateUserType.SuccessMessage, _configuration);
                }
                else
                {
                    LoggingUtlity.LogText(updateUserType.ErrorMessage, _configuration);
                }

                LoggingUtlity.LogText($"updateUserType: UserTypes with ID  updated", _configuration);
                return Ok(updateUserType);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }

        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("BulkDelete")]
        [HttpPost]
        public async Task<IActionResult> BulkDelete([FromBody] UserTypeBulkVM model)
        {
            LoggingUtlity.LogText("BulkDelete IsCalled", _configuration);

            if (!ModelState.IsValid)
            {
                return BadRequest(Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }
            LoggingUtlity.LogText("BulkDeleting Funds", _configuration);

            var deleteUserTypes = await _userTypesService.BulkDelete(model.UserTypesId);
            LoggingUtlity.LogText("BulkDeleted deleteUserTypes", _configuration);

            return Ok(deleteUserTypes);
        }

        private ObjectResult ReturnException(Exception exception, IConfiguration configuration)
        {
            LoggingUtlity.LogText(exception.ToString(), configuration);
            return StatusCode(StatusCodes.Status500InternalServerError, exception.ToString());
        }

    }
}
