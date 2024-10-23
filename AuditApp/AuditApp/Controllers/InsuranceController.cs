
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
    public class InsuranceController : ControllerBase
    {
        private readonly IInsuranceService _insuranceService;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;
        private readonly ITokenService _tokenService;
        private readonly IRequestService _requestService;

        public InsuranceController(AuditAppDBContext context,
            IInsuranceService insuranceService,
            IConfiguration configuration,
            IServiceProvider serviceProvider,
            ITokenService tokenService,
            IRequestService requestService)
        {
            _insuranceService = insuranceService;
            _configuration = configuration;
            _serviceProvider = serviceProvider;
            _tokenService = tokenService;
            _requestService = requestService;
        }

        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("CreateInsurance")]
        [HttpPost]
        public async Task<IActionResult> CreateInsurance(InsuranceVM insuranceVM)
        {
            LoggingUtlity.LogText("CreateinsuranceDetails IsCalled", _configuration);
            if (!ModelState.IsValid)
            {
                return BadRequest(Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }
            try
            {
                LoggingUtlity.LogText("Creating Insurance", _configuration);
                var insurance = await _insuranceService.CreateInsurance(insuranceVM);
                if (!string.IsNullOrEmpty(insurance.SuccessMessage))
                {
                    LoggingUtlity.LogText(insurance.SuccessMessage, _configuration);
                }
                else
                {
                    LoggingUtlity.LogText(insurance.ErrorMessage, _configuration);
                }
                return Ok(insurance);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }

        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("DeleteInsurance/{insuranceID}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteInsurance(int insuranceID)
        {
            LoggingUtlity.LogText("Deleting FundsDetails IsCalled", _configuration);
            try
            {
                var deleteInsurance = await _insuranceService.DeleteInsurance(insuranceID);
                LoggingUtlity.LogText("insurance Deleted", _configuration);
                return Ok(deleteInsurance);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }

        [Route("GetAllInsurance")]
        [HttpGet]
        public async Task<IActionResult> GetAllInsurance()
        {
            try
            {
                LoggingUtlity.LogText("Fetching All Funds Details", _configuration);

                var allInsurances = await _insuranceService.GetAllInsurance();
                LoggingUtlity.LogText("GetAllInsurance: Retrieved all insurance", _configuration);
                return Ok(allInsurances);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }

        [Route("GetInsurance/{insuranceID}")]
        [HttpGet]
        public async Task<IActionResult> GetInsurance(int insuranceID)
        {
            try
            {
                var insurance = await _insuranceService.GetInsurance(insuranceID);

                LoggingUtlity.LogText($"GetInsurance: Retrieved fund with ID", _configuration);
                return Ok(insurance);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }

        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("UpdateInsurance/{insuranceID}")]
        [HttpPost]
        public async Task<IActionResult> UpdateInsurance(int insuranceID, InsuranceVM insuranceVM)
        {
            LoggingUtlity.LogText("updateInsurance IsCalled", _configuration);
            if (!ModelState.IsValid)
            {
                return BadRequest(Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }
            try
            {
                LoggingUtlity.LogText("Updating insurance", _configuration);

                var updateInsurance = await _insuranceService.UpdateInsurance(insuranceID, insuranceVM);
                if (!string.IsNullOrEmpty(updateInsurance.SuccessMessage))
                {
                    LoggingUtlity.LogText(updateInsurance.SuccessMessage, _configuration);
                }
                else
                {
                    LoggingUtlity.LogText(updateInsurance.ErrorMessage, _configuration);
                }

                LoggingUtlity.LogText($"UpdateFund: Fund with ID updated", _configuration);
                return Ok(updateInsurance);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }

        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("BulkDelete")]
        [HttpPost]
        public async Task<IActionResult> BulkDelete([FromBody] InsuranceBulkVM model)
        {
            LoggingUtlity.LogText("BulkDelete IsCalled", _configuration);

            if (!ModelState.IsValid)
            {
                return BadRequest(Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }
            LoggingUtlity.LogText("BulkDeleting Insurances", _configuration);

            var deleteInsurancs = await _insuranceService.BulkDelete(model.InsurancesId);
            LoggingUtlity.LogText("BulkDeleted Insurances", _configuration);

            return Ok(deleteInsurancs);
        }

        private ObjectResult ReturnException(Exception exception, IConfiguration configuration)
        {
            LoggingUtlity.LogText(exception.ToString(), configuration);
            return StatusCode(StatusCodes.Status500InternalServerError, exception.ToString());
        }

    }
}
