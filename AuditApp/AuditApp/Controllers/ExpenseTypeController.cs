
using BLL;
using BLL.Interfaces;
using BLL.Services;
using DAL.Models;
using DAL.ViewModels;
using DAL.ViewModels.BulkViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Utilities;

namespace AuditApp.Controllers
{
    [ServiceFilter(typeof(AuthenticateURL))]
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseTypeController : ControllerBase
    {
        private readonly IExpenseTypeService _expenseTypeService;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;
        private readonly ITokenService _tokenService;
        private readonly IRequestService _requestService;

        public ExpenseTypeController(IExpenseTypeService expenseTypeService,
            IConfiguration configuration,
            IServiceProvider serviceProvider,
            ITokenService tokenService,
            IRequestService requestService)
        {
            _expenseTypeService = expenseTypeService;
            _configuration = configuration;
            _serviceProvider = serviceProvider;
            _tokenService = tokenService;
            _requestService = requestService;
        }

        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("CreateExpenseType")]
        [HttpPost]
        public async Task<IActionResult> CreateExpenseType([FromBody] ExpenseTypeVM expenseTypeVM)
        {
            LoggingUtlity.LogText("CreateExpenseTypeDetails IsCalled", _configuration);
            if (!ModelState.IsValid)
            {
                return BadRequest(Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }
            try
            {
                var createExpenseType = await _expenseTypeService.GetExpenseByExpenseTypeName(expenseTypeVM.ExpenseTypeName);
                if (createExpenseType == null)
                {
                    var createdExpenseType = await _expenseTypeService.CreateExpenseType(expenseTypeVM);
                    if (!string.IsNullOrEmpty(createdExpenseType.SuccessMessage))
                    {
                        LoggingUtlity.LogText(createdExpenseType.SuccessMessage, _configuration);
                    }
                    else
                    {
                        LoggingUtlity.LogText(createdExpenseType.ErrorMessage, _configuration);
                    }
                    return Ok(createdExpenseType);
                }
                else
                {
                    LoggingUtlity.LogText("expensetype alreaady exist", _configuration);
                    return Conflict("Existed");
                }
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }

        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("DeleteExpenseType/{expensetypeID}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteExpenseType(int expensetypeID)
        {
            LoggingUtlity.LogText("Deleting ExpenseDetails IsCalled", _configuration);

            try
            {
                var deletedExpenseType = await _expenseTypeService.DeleteExpenseType(expensetypeID);
                LoggingUtlity.LogText($"Deleted ExpenseType with ID: {deletedExpenseType.ExpenseTypeID}", _configuration);
                return Ok(deletedExpenseType);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }

        [Route("GetAllExpenseType")]
        [HttpGet]
        public async Task<IActionResult> GetAllExpenseType()
        {
            try
            {
                LoggingUtlity.LogText("Fetching All expenses types Details", _configuration);

                var allExpenseTypes = await _expenseTypeService.GetAllExpenseType();
                LoggingUtlity.LogText("Fetched All expense types Details", _configuration);
                return Ok(allExpenseTypes);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }

        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("GetExpenseType/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetExpenseType(int id)
        {
            try
            {
                var expenseType = await _expenseTypeService.GetExpenseType(id);
                LoggingUtlity.LogText($"Retrieved ExpenseType with ID: {expenseType.ExpenseTypeID}", _configuration);
                return Ok(expenseType);
            }
            catch (Exception ex)
            {
                return ReturnException(ex, _configuration);
            }
        }

        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("UpdateExpenseType/{expensetypeID}")]
        [HttpPut]
        public async Task<IActionResult> UpdateExpenseType(int expensetypeID, [FromBody] ExpenseTypeVM expenseTypeVM)
        {
            LoggingUtlity.LogText("UpdateExpenseTypes IsCalled", _configuration);
            if (!ModelState.IsValid)
            {
                return BadRequest(Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }
            try
            {
                LoggingUtlity.LogText("Updating expense", _configuration);

                var updatedExpenseType = await _expenseTypeService.UpdateExpenseType(expensetypeID, expenseTypeVM);

                if (!string.IsNullOrEmpty(updatedExpenseType.SuccessMessage))
                {
                    LoggingUtlity.LogText(updatedExpenseType.SuccessMessage, _configuration);
                }
                else
                {
                    LoggingUtlity.LogText(updatedExpenseType.ErrorMessage, _configuration);
                }

                LoggingUtlity.LogText($"updatedExpenseType:successfully", _configuration);
                return Ok(updatedExpenseType);
            }
            catch (Exception ex)
            {
                LoggingUtlity.ExLog(ex.ToString(), _serviceProvider);
                throw;
            }
        }

        [ServiceFilter(typeof(AuthenticateToken))]
        [Route("BulkDelete")]
        [HttpPost]
        public async Task<IActionResult> BulkDelete([FromBody] ExpenseTypeBulkVM model)
        {
            LoggingUtlity.LogText("BulkDelete IsCalled", _configuration);

            if (!ModelState.IsValid)
            {
                return BadRequest(BLL.Common.ReturnBadRequest(ModelState, _configuration, _serviceProvider));
            }
            LoggingUtlity.LogText("BulkDeleting Users", _configuration);

            var deleteExpenseTypes = await _expenseTypeService.BulkDelete(model.expenseTypeId);
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
