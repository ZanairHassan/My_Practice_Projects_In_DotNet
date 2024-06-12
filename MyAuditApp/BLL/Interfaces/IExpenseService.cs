using DAL.Models;
using DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IExpenseService
    {
        Task<Expense> CreateExpense(ExpenseVM expenseVm);
        Task<Expense> GetExpense(int expenseId);
        Task<Expense> UpdateExpense(int expenseId, ExpenseVM expenseVm);
        Task<Expense> DeleteExpense(int expenseId);
        Task<List<Expense>> GetAllExpenses();
    }
}
