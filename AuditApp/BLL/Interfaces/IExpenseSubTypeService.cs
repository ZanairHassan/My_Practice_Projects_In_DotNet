using DAL.Models;
using DAL.ViewModels;

namespace BLL.Interfaces
{
    public interface IExpenseSubTypeService
    {
        Task<ExpenseSubType> CreateExpenseSubType(ExpenseSubTypeVM expensesubtypeVM);
        Task<ExpenseSubType> GetExpenseSubType(int expensesubtypeID);
        Task<List<ExpenseSubType>> GetAllExpenseSubType();
        Task<ExpenseSubType> UpdateExpenseSubType(int expensesubtypeID, ExpenseSubTypeVM expensesubtypeVM);
        Task<ExpenseSubType> DeleteExpenseSubType(int expensesubtypeID);
    }
}
