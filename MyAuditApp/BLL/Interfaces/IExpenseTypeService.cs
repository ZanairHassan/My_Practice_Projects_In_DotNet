using DAL.Models;
using DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IExpenseTypeService
    {
        Task<ExpenseType> CreateExpenseType(ExpenseTypeVM expenseTypeVM);
        Task<ExpenseType> UpdateExpenseType(int expenseTypeId, ExpenseTypeVM expenseTypeVM);
        Task<ExpenseType> GetExpenseType(int expenetTypeId);
        Task<ExpenseType> GetAllExpenseType();
        Task<ExpenseType> DeleteExpenseType(int expenseTypeId);
        Task<ExpenseType> GetExpenseTypeByName(string expenseTypeName);
    }
}
