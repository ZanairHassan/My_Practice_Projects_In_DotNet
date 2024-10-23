using BLL;
using BLL.Interfaces;
using BLL.Services;
using DAL.ViewModels;
using DAL.ViewModels.BulkViewModels;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace AuditApp.Controllers
{
    [ServiceFilter(typeof(AuthenticateURL))]
    [Route("api/[controller]")]
    [ApiController]
    public class SoftwareController : ControllerBase
    {
        private readonly ISoftwareService _softwareService;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;
        private readonly ITokenService _tokenService;
        private readonly IRequestService _requestService;

        public SoftwareController(
            ISoftwareService softwareService,
            IConfiguration configuration,
            IServiceProvider serviceProvider,
            ITokenService tokenService,
            IRequestService requestService)
        {
            _softwareService = softwareService;
            _configuration = configuration;
            _serviceProvider = serviceProvider;
            _tokenService = tokenService;
            _requestService = requestService;
        }

        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("CreateSoftware")]
        [HttpPost]
        public async Task<IActionResult> CreateSoftware([FromBody] SoftwareVM softwareVM)
        {
            LoggingUtlity.LogText("CreateSoftwareDetails IsCalled", _configuration);
            if (!ModelState.IsValid)
            {
                return BadRequest(Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }
            try
            {
                LoggingUtlity.LogText("Creating Software", _configuration);
                var software = await _softwareService.CreateSoftware(softwareVM);
                if (!string.IsNullOrEmpty(software.SuccessMessage))
                {
                    LoggingUtlity.LogText(software.SuccessMessage, _configuration);
                }
                else
                {
                    LoggingUtlity.LogText(software.ErrorMessage, _configuration);
                }
                return Ok(software);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }

        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("DeleteSoftware/{softwareID}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteSoftware(int softwareID)
        {
            LoggingUtlity.LogText("Deleting DeleteSoftware IsCalled", _configuration);
            try
            {
                var deleteSoftware = await _softwareService.DeleteSoftware(softwareID);
                LoggingUtlity.LogText("DeleteSoftware Deleted", _configuration);
                return Ok(deleteSoftware);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }

        [Route("GetAllSoftware")]
        [HttpGet]
        public async Task<IActionResult> GetAllSoftware()
        {
            try
            {
                LoggingUtlity.LogText("Fetching  GetAllSoftware Details", _configuration);

                var allSoftware = await _softwareService.GetAllSoftware();
                LoggingUtlity.LogText("GetAllSoftware: Retrieved all ", _configuration);
                return Ok(allSoftware);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }

        [Route("GetSoftware/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetSoftware(int id)
        {
            try
            {
                var getSoftware = await _softwareService.GetSoftware(id);

                LoggingUtlity.LogText($"GetSoftware: Retrieved  with ID ", _configuration);
                return Ok(getSoftware);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }

        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("UpdateSoftware/{softwareID}")]
        [HttpPost]
        public async Task<IActionResult> UpdateSoftware(int softwareID, SoftwareVM softwareVM)
        {
            LoggingUtlity.LogText("UpdateSoftware IsCalled", _configuration);
            if (!ModelState.IsValid)
            {
                return BadRequest(Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }
            try
            {
                LoggingUtlity.LogText("Updating FUnds", _configuration);

                var updateSoftware = await _softwareService.UpdateSoftware(softwareID, softwareVM);
                if (!string.IsNullOrEmpty(updateSoftware.SuccessMessage))
                {
                    LoggingUtlity.LogText(updateSoftware.SuccessMessage, _configuration);
                }
                else
                {
                    LoggingUtlity.LogText(updateSoftware.ErrorMessage, _configuration);
                }

                LoggingUtlity.LogText($"UpdateSoftware: Fund with ID updated", _configuration);
                return Ok(updateSoftware);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }


        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("BulkDelete")]
        [HttpPost]
        public async Task<IActionResult> BulkDelete([FromBody] SoftwareBulkVM model)
        {
            LoggingUtlity.LogText("BulkDelete IsCalled", _configuration);

            if (!ModelState.IsValid)
            {
                return BadRequest(Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }
            LoggingUtlity.LogText("BulkDeleting softwares", _configuration);

            var deleteSoftwares = await _softwareService.BulkDelete(model.SoftwaresId);
            LoggingUtlity.LogText("BulkDeleted deleteSoftwares", _configuration);

            return Ok(deleteSoftwares);
        }

        private ObjectResult ReturnException(Exception exception, IConfiguration configuration)
        {
            LoggingUtlity.LogText(exception.ToString(), configuration);
            return StatusCode(StatusCodes.Status500InternalServerError, exception.ToString());
        }

    }
}
