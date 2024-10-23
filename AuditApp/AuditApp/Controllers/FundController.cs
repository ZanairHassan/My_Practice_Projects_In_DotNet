
using BLL;
using BLL.Interfaces;
using BLL.Services;
using DAL.DBContext;
using DAL.Models;
using DAL.ViewModels;
using DAL.ViewModels.BulkViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Utilities;

namespace AuditApp.Controllers
{
    [ServiceFilter(typeof(AuthenticateURL))]
    [Route("api/[controller]")]
    [ApiController]
    public class FundController : ControllerBase
    {
        private readonly IFundService _fundService;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;
        private readonly ITokenService _tokenService;
        private readonly IRequestService _requestService;

        public FundController(
            IFundService fundService,
            IConfiguration configuration,
            IServiceProvider serviceProvider,
            ITokenService tokenService,
            IRequestService requestService)
        {
            _fundService = fundService;
            _configuration = configuration;
            _serviceProvider = serviceProvider;
            _tokenService = tokenService;
            _requestService = requestService;
        }

        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("CreateFund/{fundTypeId}")]
        [HttpPost]
        public async Task<IActionResult> CreateFund([FromBody] FundVM fundVM,int fundTypeId)
        {
            LoggingUtlity.LogText("CreateFundDetails IsCalled", _configuration);
            if (!ModelState.IsValid)
            {
                return BadRequest(Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }
            try
            {
                LoggingUtlity.LogText("Creating funds", _configuration);
                var fund=await _fundService.CreateFund(fundVM, fundTypeId);
                if (!string.IsNullOrEmpty(fund.SuccessMessage))
                {
                    LoggingUtlity.LogText(fund.SuccessMessage, _configuration);
                }
                else
                {
                    LoggingUtlity.LogText(fund.ErrorMessage, _configuration);
                }
                return Ok(fund);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }

        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("DeleteFund/{fundID}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteFund(int fundID)
        {
            LoggingUtlity.LogText("Deleting FundsDetails IsCalled", _configuration);
            try
            {
                var deleteFund = await _fundService.DeleteFund(fundID);
                LoggingUtlity.LogText("Fund Deleted", _configuration);
                return Ok(deleteFund);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }

        }

        [Route("GetAllFund")]
        [HttpGet]
        public async Task<IActionResult> GetAllFund()
        {
            try
            {
                LoggingUtlity.LogText("Fetching All Funds Details", _configuration);

                var allFund = await _fundService.GetAllFund();
                LoggingUtlity.LogText("GetAllFund: Retrieved all funds", _configuration);
                return Ok(allFund);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }

        [Route("GetFund/{fundID}")]
        [HttpGet]
        public async Task<IActionResult> GetFund(int fundID)
        {
            try
            {
                var fund = await _fundService.GetFund(fundID);

                LoggingUtlity.LogText($"GetFund: Retrieved fund with ID", _configuration);
                return Ok(fund);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }

        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("UpdateFund/{fundID}")]
        [HttpPut]
        public async Task<IActionResult> UpdateFund(int fundID,[FromBody] FundVM fundVM)
        {
            LoggingUtlity.LogText("UpdateFunds IsCalled", _configuration);
            if (!ModelState.IsValid)
            {
                return BadRequest(Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }
            try
            {
                LoggingUtlity.LogText("Updating FUnds", _configuration);

                var updateFund = await _fundService.UpdateFund(fundID, fundVM);
                if (!string.IsNullOrEmpty(updateFund.SuccessMessage))
                {
                    LoggingUtlity.LogText(updateFund.SuccessMessage, _configuration);
                }
                else
                {
                    LoggingUtlity.LogText(updateFund.ErrorMessage, _configuration);
                }

                LoggingUtlity.LogText($"UpdateFund: Fund with ID updated", _configuration);
                    return Ok(updateFund);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }


        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("BulkDelete")]
        [HttpPost]
        public async Task<IActionResult> BulkDelete([FromBody] FundBulkVM model)
        {
            LoggingUtlity.LogText("BulkDelete IsCalled", _configuration);

            if (!ModelState.IsValid)
            {
                return BadRequest(Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }
            LoggingUtlity.LogText("BulkDeleting Funds", _configuration);

            var deleteFunds = await _fundService.BulkDelete(model.FundsId);
            LoggingUtlity.LogText("BulkDeleted funds", _configuration);

            return Ok(deleteFunds);
        }


        private ObjectResult ReturnException(Exception exception, IConfiguration configuration)
        {
            LoggingUtlity.LogText(exception.ToString(), configuration);
            return StatusCode(StatusCodes.Status500InternalServerError, exception.ToString());
        }
    }
}
