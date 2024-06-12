using BLL.Interfaces;
using DAL.DBContext;
using DAL.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyAuditApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;
        private readonly ApplicationDBContext _context;
        public ExpenseController(IExpenseService expenseService,
            ApplicationDBContext context)
        {
            _expenseService= expenseService;
            _context= context;
        }
        [Route("CreateExpense")]
        [HttpPost]
        public async Task<IActionResult> CreateExpense([FromBody] ExpenseVM expenseVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var expense= await _expenseService.CreateExpense(expenseVM);
            return Ok(expense);
        }
        [Route("DeleteExpense/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var deletedExpense=await _expenseService.DeleteExpense(id);
            if(deletedExpense == null)
            {
                return NotFound();
            }
            return Ok(deletedExpense);
        }
        [Route("GetAllExpense")]
        [HttpGet]
        public async Task<IActionResult> GetAllExpense()
        {
            var allExpenses= await _expenseService.GetAllExpenses();
            return Ok(allExpenses);
        }
        [Route("GetExpense/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetExpense(int id)
        {
            var expense= await _expenseService.GetExpense(id);
            if(expense == null)
            {
                return NotFound();
            }
            return Ok(expense);
        }
        [Route("UpdateExpense/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateExpense(int id,ExpenseVM expenseVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updateExpense=await _expenseService.UpdateExpense(id,expenseVM);
            if(updateExpense == null)
            {
                return NotFound();
            }
            return Ok(updateExpense);
        }
    }
}
