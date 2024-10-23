
using BLL;
using BLL.Interfaces;
using BLL.Services;
using DAL.Models;
using DAL.ViewModels;
using DAL.ViewModels.BulkViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Twilio.Jwt;
using Utilities;

namespace AuditApp.Controllers
{
    [ServiceFilter(typeof(AuthenticateURL))]
    [Route("api/[controller]")]
    [ApiController]
    public class InvestmentController : ControllerBase
    {
        private readonly IInvestmentService _investmentService;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;
        private readonly ITokenService _tokenService;
        private readonly IRequestService _requestService;
        public InvestmentController(IInvestmentService investmentService,
            IConfiguration configuration,
            IServiceProvider serviceProvider,
            ITokenService tokenService,
            IRequestService requestService)
        {
            _serviceProvider = serviceProvider;
            _configuration = configuration;
            _serviceProvider = serviceProvider;
            _tokenService = tokenService;
            _requestService = requestService;
        }

        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("CreateInvestment/{investTypeId}")]
        [HttpPost]
        public async Task<IActionResult> CreateInvestment(InvestmentVM investmentVM,int investTypeId)
        {
            LoggingUtlity.LogText("CreateInvestmentDetails IsCalled", _configuration);
            if (!ModelState.IsValid)
            {
                return BadRequest(Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }
            try
            {
                LoggingUtlity.LogText("Creating investment", _configuration);
                var invest = await _investmentService.CreateInvestment(investmentVM, investTypeId);
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
        [Route("DeleteInvestment/{investmentID}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteInvestment(int investmentID )
        {
            LoggingUtlity.LogText("Deleting InvestmentDetails IsCalled", _configuration);
            try
            {
                var deleteInvestment = await _investmentService.DeleteInvestmentById(investmentID);
                LoggingUtlity.LogText("Investment Deleted", _configuration);
                return Ok(deleteInvestment);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }
        [Route("GetAllInvestments")]
        [HttpGet]
        public async Task<IActionResult> GetAllInvestments()
        {
            try
            {
                LoggingUtlity.LogText("Fetching All invest Details", _configuration);

                var allInvest = await _investmentService.GetAllInvestMents();
                LoggingUtlity.LogText("GetAllinvest: Retrieved all", _configuration);
                return Ok(allInvest);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }

        [Route("GetInvestment/{investmentID}")]
        [HttpGet]
        async Task<IActionResult> GetInvestment(int investmentID)
        {
            try
            {
                var invest = await _investmentService.GetInvestmentById(investmentID);

                LoggingUtlity.LogText($"GetInvest: Retrieved  with ID ", _configuration);
                return Ok(invest);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }
        [Route("UpdateInvestment/{investmentId}")]
        [HttpPut]
        public async Task<IActionResult> UpdateInvestment(int investmentId, InvestmentVM investmentVM)
        {
            LoggingUtlity.LogText("UpdateInvestment IsCalled", _configuration);
            if (!ModelState.IsValid)
            {
                return BadRequest(Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }
            try
            {
                LoggingUtlity.LogText("Updating UpdateInvestment", _configuration);

                var updateInvest = await _investmentService.UpdateInvestment(investmentId, investmentVM);
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
        public async Task<IActionResult> BulkDelete([FromBody] InvestmentBulkVM model)
        {
            LoggingUtlity.LogText("BulkDelete IsCalled", _configuration);

            if (!ModelState.IsValid)
            {
                return BadRequest(Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }
            LoggingUtlity.LogText("BulkDeleting InvestmentBulkVM", _configuration);

            var deleteInvests = await _investmentService.BulkDelete(model.InvestmentId);
            LoggingUtlity.LogText("BulkDeleted InvestmentBulkVM", _configuration);

            return Ok(deleteInvests);
        }

        private ObjectResult ReturnException(Exception exception, IConfiguration configuration)
        {
            LoggingUtlity.LogText(exception.ToString(), configuration);
            return StatusCode(StatusCodes.Status500InternalServerError, exception.ToString());
        }

    }
}
