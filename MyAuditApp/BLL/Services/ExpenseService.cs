using BLL.Interfaces;
using DAL.DBContext;
using DAL.Models;
using DAL.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly ApplicationDBContext _Context;
        public ExpenseService(ApplicationDBContext context)
        {
            _Context = context;
        }
        public async Task<Expense> CreateExpense(ExpenseVM expenseVm)
        {
            var expense = new Expense
            {
                ExpenseName=expenseVm.ExpenseName,
                ExpenseDescription=expenseVm.ExpenseDescription,
                ExpenseAmmount=expenseVm.ExpenseAmmount,
                ExpenseTypeId=expenseVm.ExpenseTypeId,
                ExpenseSubTypeId=expenseVm.ExpenseSubTypedId,
                ExpenseCreatedBy=expenseVm.UserId,
                ExpenseCreatedDate=DateTime.Now,
                IsDeleted=false
            };
            _Context.Expenses.Add(expense);
            await _Context.SaveChangesAsync();
            return expense;
        }

        public async Task<Expense> DeleteExpense(int expenseId)
        {
            var expense= await _Context.Expenses.FirstOrDefaultAsync(e=>e.ExpenseId==expenseId && !e.IsDeleted);
            if(expense == null)
            {
                return null;
            }
            expense.IsDeleted = true;
            expense.ExpenseDeletedDate = DateTime.Now;
            _Context.Expenses.Update(expense);
            await _Context.SaveChangesAsync();
            return expense;
        }

        public async Task<List<Expense>> GetAllExpenses()
        {
            return await _Context.Expenses.Where(e=> !e.IsDeleted).ToListAsync();
        }

        public async Task<Expense> GetExpense(int expenseId)
        {
            return await _Context.Expenses.FirstOrDefaultAsync(e=>e.ExpenseId==expenseId && !e.IsDeleted);
        }

        public async Task<Expense> UpdateExpense(int expenseId, ExpenseVM expenseVm)
        {
            var expense=await _Context.Expenses.FirstOrDefaultAsync(e=> e.ExpenseId==expenseId && !e.IsDeleted);
            if(expense is null)
            {
                return null;
            }
            expense.ExpenseName=expenseVm.ExpenseName;
            expense.ExpenseDescription=expenseVm.ExpenseDescription;
            expense.ExpenseAmmount=expenseVm.ExpenseAmmount;
            expense.ExpenseTypeId=expenseVm.ExpenseTypeId;
            expense.ExpenseSubTypeId=expense.ExpenseSubTypeId;
            expense.ExpenseUpdatedDate=DateTime.Now;
            expense.ExpenseUpdatedBy=expenseVm.UserId;

            _Context.Expenses.Update(expense);
            await _Context.SaveChangesAsync();
            return expense;

        }
    }
}
