using BLL.Interfaces;
using DAL.DBContext;
using DAL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Utilities;
using Microsoft.AspNetCore.Http;
using BLL;
using BLL.Services;
using DAL.ViewModels.BulkViewModels;

namespace AuditApp.Controllers
{
    [ServiceFilter(typeof(AuthenticateURL))] 
    [Route("api/[controller]")]
    [ApiController]
    public class MobileCompanyController : ControllerBase
    {
        private readonly IMobileCompanyService _mobileCompanyService;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;
        private readonly ITokenService _tokenService;
        private readonly IRequestService _requestService;

        public MobileCompanyController(
            IMobileCompanyService mobileCompanyService,
            IConfiguration configuration,
            IServiceProvider serviceProvider,
            ITokenService tokenService,
            IRequestService requestService)
        {
            _mobileCompanyService = mobileCompanyService;
            _configuration = configuration;
            _serviceProvider = serviceProvider;
            _tokenService = tokenService;
            _requestService = requestService;
        }


        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("CreateMobileCompany")]
        [HttpPost]
        public async Task<IActionResult> CreateMobileCompany([FromBody] MobileCompanyVM mobileCompanyVM)
        {
            LoggingUtlity.LogText("CreateMobileCompany: IsCalled", _configuration);

            if (!ModelState.IsValid)
            {
                return BadRequest(Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }

            try
            {
                LoggingUtlity.LogText("Creating Mobile Company", _configuration);
                var result = await _mobileCompanyService.CreateMobileCompany(mobileCompanyVM);

                if (!string.IsNullOrEmpty(result.SuccessMessage))
                {
                    LoggingUtlity.LogText(result.SuccessMessage, _configuration);
                }
                else
                {
                    LoggingUtlity.LogText(result.ErrorMessage, _configuration);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return ReturnException(ex);
            }
        }

        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("DeleteMobileCompany/{mobilecompanyID}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteMobileCompany(int mobilecompanyID)
        {
            LoggingUtlity.LogText("DeleteMobileCompany: IsCalled", _configuration);
            try
            {
                var deleteMobileCompany = await _mobileCompanyService.DeleteMobileCompany(mobilecompanyID);
                LoggingUtlity.LogText("Mobile Company Deleted", _configuration);
                return Ok(deleteMobileCompany);
            }
            catch (Exception ex)
            {
                return ReturnException(ex);
            }
        }

        [Route("GetAllMobileCompany")]
        [HttpGet]
        public async Task<IActionResult> GetAllMobileCompany()
        {
            try
            {
                LoggingUtlity.LogText("GetAllMobileCompany: IsCalled", _configuration);

                var allMobileCompanies = await _mobileCompanyService.GetAllMobileCompany();
                LoggingUtlity.LogText("GetAllMobileCompany: Retrieved all mobile companies", _configuration);
                return Ok(allMobileCompanies);
            }
            catch (Exception ex)
            {
                return ReturnException(ex);
            }
        }

        [Route("GetMobileCompany/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetMobileCompany(int id)
        {
            try
            {
                var mobileCompany = await _mobileCompanyService.GetMobileCompany(id);

                if (mobileCompany == null)
                {
                    LoggingUtlity.LogText($"GetMobileCompany: Mobile Company with ID {id} not found", _configuration);
                    return NotFound();
                }

                LoggingUtlity.LogText($"GetMobileCompany: Retrieved Mobile Company with ID {id}", _configuration);
                return Ok(mobileCompany);
            }
            catch (Exception ex)
            {
                return ReturnException(ex);
            }
        }

        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("UpdateMobileCompany/{mobilecompanyID}")]
        [HttpPost]
        public async Task<IActionResult> UpdateMobileCompany(int mobilecompanyID, [FromBody] MobileCompanyVM mobileCompanyVM)
        {
            LoggingUtlity.LogText("UpdateMobileCompany: IsCalled", _configuration);

            if (!ModelState.IsValid)
            {
                return BadRequest(Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }

            try
            {
                LoggingUtlity.LogText("Updating Mobile Company", _configuration);

                var updateMobileCompany = await _mobileCompanyService.UpdateMobileCompany(mobilecompanyID, mobileCompanyVM);
                if (!string.IsNullOrEmpty(updateMobileCompany.SuccessMessage))
                {
                    LoggingUtlity.LogText(updateMobileCompany.SuccessMessage, _configuration);
                }
                else
                {
                    LoggingUtlity.LogText(updateMobileCompany.ErrorMessage, _configuration);
                }

                LoggingUtlity.LogText($"UpdateMobileCompany: Mobile Company with ID {mobilecompanyID} updated", _configuration);
                return Ok(updateMobileCompany);
            }
            catch (Exception ex)
            {
                return ReturnException(ex);
            }
        }

        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("BulkDelete")]
        [HttpPost]
        public async Task<IActionResult> BulkDelete([FromBody] MobileCompanyBulkVM model)
        {
            LoggingUtlity.LogText("BulkDelete IsCalled", _configuration);

            if (!ModelState.IsValid)
            {
                return BadRequest(Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }
            LoggingUtlity.LogText("BulkDeleting Mobile", _configuration);

            var deleteMBL = await _mobileCompanyService.BulkDelete(model.MblCompaniesId);
            LoggingUtlity.LogText("BulkDeleted MobleCMPNY", _configuration);

            return Ok(deleteMBL);
        }

        private ObjectResult ReturnException(Exception exception)
        {
            LoggingUtlity.LogText(exception.ToString(), _configuration);
            return StatusCode(StatusCodes.Status500InternalServerError, exception.ToString());
        }
    }
}
