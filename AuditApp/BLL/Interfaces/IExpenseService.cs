using DAL.Models;
using DAL.ViewModels;

namespace BLL.Interfaces
{
    public interface IExpenseService
    {
        Task<ExpenseVM> CreateExpense(ExpenseVM expenseVM, int expenseTypeId,int expenseSubTypeId);
        Task<Expense> GetExpense(int expenseID);
        Task<List<Expense>> GetAllExpense();
        Task<ExpenseVM> UpdateExpense(int expenseID, ExpenseVM expenseVM);
        Task<Expense> DeleteExpense(int expenseID);
        Task<List<Expense>> BulkDelete(List<int> expenseId);
    }
}
