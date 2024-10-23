using DAL.Models;
using DAL.ViewModels;

namespace BLL.Interfaces
{
    public interface IExpenseTypeService
    {
        Task<ExpenseTypeVM> CreateExpenseType(ExpenseTypeVM expensetypeVM);
        Task<ExpenseType> GetExpenseType(int expensetypeID);
        Task<List<ExpenseType>> GetAllExpenseType();
        Task<ExpenseTypeVM> UpdateExpenseType(int expensetypeID, ExpenseTypeVM expensetypeVM);
        Task<ExpenseType> DeleteExpenseType(int expensetypeID);
        Task<ExpenseType> GetExpenseByExpenseTypeName(string expenseTypeName);
        Task<List<ExpenseType>> BulkDelete(List<int> expenseTypeId);
    }
}
