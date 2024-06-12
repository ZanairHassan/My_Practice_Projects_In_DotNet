using DAL.Models;
using DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IExpenseSubTypeService
    {
        Task<ExpenseSubType> CreateExpenseSubType(ExpenseSubTypeVM expenseSubTypeVM);
        Task<ExpenseSubType> UpdateExpenseSubType(int expenseSubTypeId,ExpenseSubTypeVM expenseSubTypeVM,ExpenseSubType expenseSubType);
        Task<ExpenseSubType> DeleteExpenseSubType(int expenseSubTypeId);
        Task<ExpenseSubType> GetExpenseSubType(int expenseSubTypeId);
        Task<ExpenseSubType> GetAllExpenseSubtype();
    }
}
