
using BLL;
using BLL.Interfaces;
using BLL.Services;
using DAL.Models;
using DAL.ViewModels;
using DAL.ViewModels.BulkViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace AuditApp.Controllers
{
    [ServiceFilter(typeof(AuthenticateURL))]
    [Route("api/[controller]")]
    [ApiController]
    public class InvestmentTypeController : ControllerBase
    {
        private readonly IInvestmentTypeService _investmentTypeService;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;
        private readonly ITokenService _tokenService;
        private readonly IRequestService _requestService;
        public InvestmentTypeController(
            IInvestmentTypeService investmentTypeService,
            IConfiguration configuration,
            IServiceProvider serviceProvider,
            ITokenService tokenService,
            IRequestService requestService
            )
        {
            _investmentTypeService = investmentTypeService;
            _configuration = configuration;
            _serviceProvider = serviceProvider;
            _tokenService = tokenService;
            _requestService = requestService;
        }

        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("CreateInvestmentType")]
        [HttpPost]
        public async Task<IActionResult> CreateInvestmentType([FromBody] InvestmentTypeVM investmentTypeVM)
        {
            LoggingUtlity.LogText("CreateInvestmentDetails IsCalled", _configuration);
            if (!ModelState.IsValid)
            {
                return BadRequest(Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }
            try
            {
                LoggingUtlity.LogText("Creating investmentTYpe", _configuration);
                var invest = await _investmentTypeService.CreateInvestmentType(investmentTypeVM);
                if (!string.IsNullOrEmpty(invest.SuccessMessage))
                {
                    LoggingUtlity.LogText(invest.SuccessMessage, _configuration);
                }
                else
                {
                    LoggingUtlity.LogText(invest.ErrorMessage, _configuration);
                }
                return Ok(invest);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }

        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("DeleteInvestmentType/{investmentTypeId}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteInvestmentType(int investmentTypeId,InvestmentTypeVM investmentTypeVM)
        {
            LoggingUtlity.LogText("Deleting InvestmentTypeDetails IsCalled", _configuration);
            try
            {
                var deleteInvestment = await _investmentTypeService.DeleteInvestmentType(investmentTypeId);
                LoggingUtlity.LogText("Investment type Deleted", _configuration);
                return Ok(deleteInvestment);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }
        [Route("GetAllInvestMent")]
        [HttpGet]
        public async Task<IActionResult> GetAllInvestMent()
        {
            try
            {
                LoggingUtlity.LogText("Fetching All invest type Details", _configuration);

                var allInvest = await _investmentTypeService.GetAllInvestmentTypes();
                LoggingUtlity.LogText("GetAllinvest: Retrieved all", _configuration);
                return Ok(allInvest);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }

        [Route("GetInvestmentType/{investmentTypeId}")]
        [HttpGet]
        public async Task<IActionResult> GetInvestmentType(int investmentTypeId )
        {
            try
            {
                var invest = await _investmentTypeService.GetInvestmentType(investmentTypeId);

                LoggingUtlity.LogText($"investmentType: Retrieved  with ID ", _configuration);
                return Ok(invest);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }
        [Route("UpdateInvestment/{investmentTypeId}")]
        [HttpPost]
        public async Task<IActionResult> UpdateInvestment(InvestmentTypeVM investmentTypeVM, int investmentTypeId)
        {
            LoggingUtlity.LogText("UpdateInvestment IsCalled", _configuration);
            if (!ModelState.IsValid)
            {
                return BadRequest(Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }
            try
            {
                LoggingUtlity.LogText("Updating UpdateInvestment type", _configuration);

                var updateInvest = await _investmentTypeService.UpdateInvestMentType( investmentTypeVM,investmentTypeId);
                if (!string.IsNullOrEmpty(updateInvest.SuccessMessage))
                {
                    LoggingUtlity.LogText(updateInvest.SuccessMessage, _configuration);
                }
                else
                {
                    LoggingUtlity.LogText(updateInvest.ErrorMessage, _configuration);
                }

                LoggingUtlity.LogText($"UpdateUpdateInvestment:  with ID updated", _configuration);
                return Ok(updateInvest);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }

        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("BulkDelete")]
        [HttpPost]
        public async Task<IActionResult> BulkDelete([FromBody] InvestmentTypeBulkVM model)
        {
            LoggingUtlity.LogText("BulkDelete IsCalled", _configuration);

            if (!ModelState.IsValid)
            {
                return BadRequest(Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }
            LoggingUtlity.LogText("BulkDeleting InvestTypes", _configuration);

            var deleteFunds = await _investmentTypeService.BulkDelete(model.InvestmentTypesId);
            LoggingUtlity.LogText("BulkDeleted investTypes", _configuration);

            return Ok(deleteFunds);
        }

        private ObjectResult ReturnException(Exception exception, IConfiguration configuration)
        {
            LoggingUtlity.LogText(exception.ToString(), configuration);
            return StatusCode(StatusCodes.Status500InternalServerError, exception.ToString());
        }

    }

}
