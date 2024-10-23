
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
    public class IncomeController : ControllerBase
    {
        private readonly IIncomeService _incomeService;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;
        private readonly ITokenService _tokenService;
        private readonly IRequestService _requestService;

        public IncomeController(
            IIncomeService incomeService,
            IConfiguration configuration,
            IServiceProvider serviceProvider,
            ITokenService tokenService,
            IRequestService requestService)
        {
            _incomeService = incomeService;
            _configuration = configuration;
            _serviceProvider = serviceProvider;
            _tokenService = tokenService;
            _requestService = requestService;
        }

        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("CreateIncome/{incomeTypeId}")]
        [HttpPost]
        public async Task<IActionResult> CreateIncome([FromBody]IncomeVM incomeVM,int incomeTypeId)
        {
            LoggingUtlity.LogText("CreateincomeDetails IsCalled", _configuration);
            if (!ModelState.IsValid)
            {
                return BadRequest(Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }
            try
            {
                LoggingUtlity.LogText("Creating income", _configuration);
                var income = await _incomeService.CreateIncome(incomeVM,incomeTypeId);
                if (!string.IsNullOrEmpty(income.SuccessMessage))
                {
                    LoggingUtlity.LogText(income.SuccessMessage, _configuration);
                }
                else
                {
                    LoggingUtlity.LogText(income.ErrorMessage, _configuration);
                }
                return Ok(income);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }

        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("DeleteIncome/{incomeID}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteIncome(int incomeID)
        {
            LoggingUtlity.LogText("Deleting IncomeDetails IsCalled", _configuration);
            try
            {
                var deleteIncome = await _incomeService.DeleteIncome(incomeID);
                LoggingUtlity.LogText("Income Deleted", _configuration);
                return Ok(deleteIncome);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }

        [Route("GetAllIncome")]
        [HttpGet]
        public async Task<IActionResult> GetAllIncome()
        {
            try
            {
                LoggingUtlity.LogText("Fetching All Income Details", _configuration);

                var allIncomes = await _incomeService.GetAllIncome();
                LoggingUtlity.LogText("GetAllIncomes: Retrieved all Incomes", _configuration);
                return Ok(allIncomes);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }

        [Route("GetIncome/{incomeID}")]
        [HttpGet]
        public async Task<IActionResult> GetIncome(int incomeID)
        {
            try
            {
                var income = await _incomeService.GetIncome(incomeID);

                LoggingUtlity.LogText($"GetIncome: Retrieved income with ID", _configuration);
                return Ok(income);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }

        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("UpdateIncome/{incomeID}")]
        [HttpPost]
        public async Task<IActionResult> UpdateIncome(int incomeID,[FromBody] IncomeVM incomeVM)
        {
            LoggingUtlity.LogText("UpdateIncome IsCalled", _configuration);
            if (!ModelState.IsValid)
            {
                return BadRequest(Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }
            try
            {
                LoggingUtlity.LogText("Updating income", _configuration);

                var updateIncome = await _incomeService.UpdateIncome(incomeID, incomeVM);
                if (!string.IsNullOrEmpty(updateIncome.SuccessMessage))
                {
                    LoggingUtlity.LogText(updateIncome.SuccessMessage, _configuration);
                }
                else
                {
                    LoggingUtlity.LogText(updateIncome.ErrorMessage, _configuration);
                }

                LoggingUtlity.LogText($"UpdateFund: Fund with ID has updated", _configuration);
                return Ok(updateIncome);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }


        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("BulkDelete")]
        [HttpPost]
        public async Task<IActionResult> BulkDelete([FromBody] IncomeBulkVM model)
        {
            LoggingUtlity.LogText("BulkDelete IsCalled", _configuration);

            if (!ModelState.IsValid)
            {
                return BadRequest(Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }
            LoggingUtlity.LogText("BulkDeleting Incomes", _configuration);

            var deleteIncomes = await _incomeService.BulkDelete(model.IncomesId);
            LoggingUtlity.LogText("BulkDeleted Incomes", _configuration);

            return Ok(deleteIncomes);
        }


        private ObjectResult ReturnException(Exception exception, IConfiguration configuration)
        {
            LoggingUtlity.LogText(exception.ToString(), configuration);
            return StatusCode(StatusCodes.Status500InternalServerError, exception.ToString());
        }

    }
}
