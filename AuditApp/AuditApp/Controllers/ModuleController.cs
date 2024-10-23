
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
    public class ModuleController : ControllerBase
    {
        private readonly IModuleService _moduleService;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;
        private readonly ITokenService _tokenService;
        private readonly IRequestService _requestService;

        public ModuleController(
            IModuleService moduleService,
            IConfiguration configuration,
            IServiceProvider serviceProvider,
            ITokenService tokenService,
            IRequestService requestService)
        {
            _moduleService = moduleService;
            _configuration = configuration;
            _serviceProvider = serviceProvider;
            _tokenService = tokenService;
            _requestService = requestService;
        }

        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("CreateModule")]
        [HttpPost]
        public async Task<IActionResult> CreateModule([FromBody] ModuleVM moduleVM)
        {
            LoggingUtlity.LogText("CreateFUndDetails IsCalled", _configuration);
            if (!ModelState.IsValid)
            {
                return BadRequest(Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }
            try
            {
                LoggingUtlity.LogText("Creating Module", _configuration);
                var module = await _moduleService.CreateModule(moduleVM);
                if (!string.IsNullOrEmpty(module.SuccessMessage))
                {
                    LoggingUtlity.LogText(module.SuccessMessage, _configuration);
                }
                else
                {
                    LoggingUtlity.LogText(module.ErrorMessage, _configuration);
                }
                return Ok(module);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }

        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("DeleteModule/{moduleID}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteModule(int moduleID)
        {
            LoggingUtlity.LogText("Deleting Module IsCalled", _configuration);
            try
            {
                var deleteModule = await _moduleService.DeleteModule(moduleID);
                LoggingUtlity.LogText("Module Deleted", _configuration);
                return Ok(deleteModule);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }

        [Route("GetAllModule")]
        [HttpGet]
        public async Task<IActionResult> GetAllModule()
        {
            try
            {
                LoggingUtlity.LogText("Fetching All Module Details", _configuration);

                var allModule = await _moduleService.GetAllModule();
                LoggingUtlity.LogText("allModule: Retrieved  allModule", _configuration);
                return Ok(allModule);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }

        [Route("GetModule/{id}")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetModule(int id)
        {
            try
            {
                var module = await _moduleService.GetModule(id);

                LoggingUtlity.LogText($"module: Retrieved module with ID", _configuration);
                return Ok(module);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }

        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("UpdateModule/{moduleID}")]
        [HttpPost]
        public async Task<IActionResult> UpdateModule(int moduleID, ModuleVM moduleVM)
        {
            LoggingUtlity.LogText("UpdateFunds IsCalled", _configuration);
            if (!ModelState.IsValid)
            {
                return BadRequest(Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }
            try
            {
                LoggingUtlity.LogText("Updating module", _configuration);

                var updatemodule = await _moduleService.UpdateModule(moduleID, moduleVM);
                if (!string.IsNullOrEmpty(updatemodule.SuccessMessage))
                {
                    LoggingUtlity.LogText(updatemodule.SuccessMessage, _configuration);
                }
                else
                {
                    LoggingUtlity.LogText(updatemodule.ErrorMessage, _configuration);
                }

                LoggingUtlity.LogText($"updatemodule: Fund with ID updated", _configuration);
                return Ok(updatemodule);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }

        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("BulkDelete")]
        [HttpPost]
        public async Task<IActionResult> BulkDelete([FromBody] ModuleBulkVM model)
        {
            LoggingUtlity.LogText("BulkDelete IsCalled", _configuration);

            if (!ModelState.IsValid)
            {
                return BadRequest(Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }
            LoggingUtlity.LogText("BulkDeleting deleteModules", _configuration);

            var deleteModules = await _moduleService.BulkDelete(model.ModulesId);
            LoggingUtlity.LogText("BulkDeleted deleteModules", _configuration);

            return Ok(deleteModules);
        }

        private ObjectResult ReturnException(Exception exception, IConfiguration configuration)
        {
            LoggingUtlity.LogText(exception.ToString(), configuration);
            return StatusCode(StatusCodes.Status500InternalServerError, exception.ToString());
        }
    }
}
