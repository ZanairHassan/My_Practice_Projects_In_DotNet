
using BLL;
using BLL.Interfaces;
using BLL.Services;
using DAL.Models;
using DAL.ViewModels;
using DAL.ViewModels.BulkViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Utilities;

namespace AuditApp.Controllers
{
    [ServiceFilter(typeof(AuthenticateURL))]
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseservice;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;
        private readonly IRequestService _requestService;
        private readonly ITokenService _tokenService;

        public ExpenseController(IExpenseService expenseService,
            IConfiguration configuration,
            IServiceProvider serviceProvider,
            IRequestService requestService,
            ITokenService tokenService
            )
        {
            _expenseservice = expenseService;
            _configuration = configuration;
            _serviceProvider = serviceProvider;
            _requestService = requestService;
            _tokenService = tokenService;
        }

        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("CreateExpense/{expenseTypeId}/{expenseSubTypeId}")]
        [HttpPost]
        public async Task<IActionResult> CreateExpense([FromBody] ExpenseVM expenseVM, int expenseTypeId, int expenseSubTypeId)
        {

            LoggingUtlity.LogText("CreateExpenseDetails IsCalled", _configuration);
            if (!ModelState.IsValid)
            {
                return BadRequest(Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }
            try
            {
                LoggingUtlity.LogText("Creating Expense", _configuration);
                var expense = await _expenseservice.CreateExpense(expenseVM,expenseTypeId, expenseSubTypeId);
                if (!string.IsNullOrEmpty(expense.SuccessMessage))
                {
                    LoggingUtlity.LogText(expense.SuccessMessage, _configuration);
                }
                else
                {
                    LoggingUtlity.LogText(expense.ErrorMessage, _configuration);
                }
                return Ok(expense);


            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }

        }

        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("DeleteExpense/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            LoggingUtlity.LogText("Deleting ExpenseDetails IsCalled", _configuration);
            try
            {
                var deletedExpense = await _expenseservice.DeleteExpense(id);

                LoggingUtlity.LogText("Expense Deleted", _configuration);
                return Ok(deletedExpense);
            }
            
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }

        [Route("GetAllExpense")]
        [HttpGet]
        public async Task<IActionResult> GetAllExpense()
        {

            try
            {
                LoggingUtlity.LogText("Fetching All expenses Details", _configuration);
                var allExpenses = await _expenseservice.GetAllExpense();
                LoggingUtlity.LogText("Fetched All expense Details", _configuration);
                return Ok(allExpenses);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }

        }

        [Route("GetExpense/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetExpense(int id)
        {
            try
            {
                var expense = await _expenseservice.GetExpense(id);
                LoggingUtlity.LogText($"GetExpense: Expense with ID {id} returned successfully", _configuration);
                return Ok(expense);

            }

            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }

        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("UpdateExpense/{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateExpense(int id, [FromBody] ExpenseVM expenseVM)
        {
            LoggingUtlity.LogText("UpdateExpense IsCalled", _configuration);

            if (!ModelState.IsValid)
            {
                return BadRequest(Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }
            try
            {
                LoggingUtlity.LogText("Updating expense", _configuration);
                var updatedExpense = await _expenseservice.UpdateExpense(id, expenseVM);

                if (!string.IsNullOrEmpty(updatedExpense.SuccessMessage))
                {
                    LoggingUtlity.LogText(updatedExpense.SuccessMessage, _configuration);
                }
                else
                {
                    LoggingUtlity.LogText(updatedExpense.ErrorMessage, _configuration);
                }

                LoggingUtlity.LogText($"UpdateExpense: Expense with ID {id} updated successfully", _configuration);
                return Ok(updatedExpense);
                

            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }

        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("BulkDelete")]
        [HttpPost]
        public async Task<IActionResult> BulkDelete([FromBody] ExpenseBulkVM model)
        {
            LoggingUtlity.LogText("BulkDelete IsCalled", _configuration);

            if (!ModelState.IsValid)
            {
                return BadRequest(Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }
            LoggingUtlity.LogText("BulkDeleting Users", _configuration);

            var deleteExpenseTypes = await _expenseservice.BulkDelete(model.expenseIds);
            LoggingUtlity.LogText("BulkDeleted Users", _configuration);

            return Ok(deleteExpenseTypes);
        }


        private ObjectResult ReturnException(Exception exception, IConfiguration configuration)
        {
            LoggingUtlity.LogText(exception.ToString(), configuration);
            return StatusCode(StatusCodes.Status500InternalServerError, exception.ToString());
        }
    }
}

