
using BLL;
using BLL.Interfaces;
using BLL.Services;
using DAL.DBContext;
using DAL.Models;
using DAL.ViewModels;
using DAL.ViewModels.BulkViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Utilities;

namespace AuditApp.Controllers
{
    [ServiceFilter(typeof(AuthenticateURL))]
    [Route("api/[controller]")]
    [ApiController]
    public class ModulePermissionController : ControllerBase
    {
        private readonly IModulePermissionService _modulePermissionService;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;
        private readonly ITokenService _tokenService;
        private readonly IRequestService _requestService;

        public ModulePermissionController(AuditAppDBContext context,
            IModulePermissionService modulePermissionService,
            IConfiguration configuration,
            IServiceProvider serviceProvider,
            ITokenService tokenService,
            IRequestService requestService)
        {
            _modulePermissionService = modulePermissionService;
            _configuration = configuration;
            _serviceProvider = serviceProvider;
            _tokenService = tokenService;
            _requestService = requestService;
        }

        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("CreateModulePermission")]
        [HttpPost]
        public async Task<IActionResult> CreateModulePermission([FromBody] ModulePermissionVM modulePermissionVM)
        {
            LoggingUtlity.LogText("CreateModulePermission IsCalled", _configuration);
            if (!ModelState.IsValid)
            {
                return BadRequest(Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }
            try
            {
                LoggingUtlity.LogText("Creating mdlp", _configuration);
                var mdlp = await _modulePermissionService.CreateModulePermssion(modulePermissionVM);
                if (!string.IsNullOrEmpty(mdlp.SuccessMessage))
                {
                    LoggingUtlity.LogText(mdlp.SuccessMessage, _configuration);
                }
                else
                {
                    LoggingUtlity.LogText(mdlp.ErrorMessage, _configuration);
                }
                return Ok(mdlp);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }

        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("DeleteModulePermission/{modulepermissionID}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteModulePermission(int modulepermissionID )
        {
            LoggingUtlity.LogText("Deleting DeleteModulePermission IsCalled", _configuration);
            try
            {
                var deletemp = await _modulePermissionService.DeleteModulePermission(modulepermissionID);
                LoggingUtlity.LogText("DeleteModulePermission Deleted", _configuration);
                return Ok(deletemp);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }

        [Route("GetAllModulePermission")]
        [HttpGet]
        public async Task<IActionResult> GetAllModulePermission()
        {
            try
            {
                LoggingUtlity.LogText("Fetching All GetAllModulePermission Details", _configuration);

                var allmp = await _modulePermissionService.GetAllModulePermission();
                LoggingUtlity.LogText("GetAllModulePermission: Retrieved all funds", _configuration);
                return Ok(allmp);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }

        [Route("GetModulePermission/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetModulePermission(int id)
        {
            try
            {
                var md = await _modulePermissionService.GetModulePermission(id);

                LoggingUtlity.LogText($"GetModulePermission: Retrieved", _configuration);
                return Ok(md);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }

        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("UpdateModulePermission/{modulepermissionID}")]
        [HttpPost]
        public async Task<IActionResult> UpdateModulePermission(int modulepermissionID, ModulePermissionVM modulePermissionVM)
        {
            LoggingUtlity.LogText("UpdateModulePermission IsCalled", _configuration);
            if (!ModelState.IsValid)
            {
                return BadRequest(Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }
            try
            {
                LoggingUtlity.LogText(" UpdateModulePermission", _configuration);

                var updatemp = await _modulePermissionService.UpdateModulePermission(modulepermissionID, modulePermissionVM);
                if (!string.IsNullOrEmpty(updatemp.SuccessMessage))
                {
                    LoggingUtlity.LogText(updatemp.SuccessMessage, _configuration);
                }
                else
                {
                    LoggingUtlity.LogText(updatemp.ErrorMessage, _configuration);
                }

                LoggingUtlity.LogText($"UpdateModulePermission:  with ID updated", _configuration);
                return Ok(updatemp);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }

        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("BulkDelete")]
        [HttpPost]
        public async Task<IActionResult> BulkDelete([FromBody] ModPermissionBulkVM model)
        {
            LoggingUtlity.LogText("BulkDelete IsCalled", _configuration);

            if (!ModelState.IsValid)
            {
                return BadRequest(Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }
            LoggingUtlity.LogText("BulkDeleting deleteModPers", _configuration);

            var deleteModPers = await _modulePermissionService.BulkDelete(model.ModPermissionId);
            LoggingUtlity.LogText("BulkDeleted deleteModPers", _configuration);

            return Ok(deleteModPers);
        }

        private ObjectResult ReturnException(Exception exception, IConfiguration configuration)
        {
            LoggingUtlity.LogText(exception.ToString(), configuration);
            return StatusCode(StatusCodes.Status500InternalServerError, exception.ToString());
        }

    }
}
