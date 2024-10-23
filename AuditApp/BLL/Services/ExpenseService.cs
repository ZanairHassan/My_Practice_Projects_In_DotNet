using BLL.Interfaces;
using DAL.DBContext;
using DAL.Models;
using DAL.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly AuditAppDBContext _context;

        public ExpenseService(AuditAppDBContext context)
        {
            _context = context;
        }

        public async Task<ExpenseVM> CreateExpense(ExpenseVM expenseVM, int expenseTypeId,int expenseSubTypeId)
        {
            var expenseType = await _context.ExpenseTypes.FirstOrDefaultAsync(b => b.ExpenseTypeID == expenseTypeId);
            if (expenseType == null)
            {
                throw new Exception($"expenseType with ID not found.");
            }

            var expenseSubType = await _context.ExpenseSubTypes.FirstOrDefaultAsync(b => b.ExpenseSubTypeId == expenseSubTypeId);
            if (expenseType == null)
            {
                throw new Exception($"expenseType with ID not found.");
            }

            if (expenseVM == null)
            {
                throw new ArgumentNullException(nameof(expenseVM), "expenseVM cannot be null");
            }
            //ValidateExpenseDetailsVM(expenseVM);

            var expense = new Expense
            {
                ExpenseName = expenseVM.ExpenseName,
                ExpenseDescription = expenseVM.ExpenseDescription,
                ExpenseAmount = expenseVM.ExpenseAmount,
                ExpenseTypeName = expenseType.ExpenseTypeName,
                ExpenseSubTypeName = expenseSubType.ExpenseSubTypeName,
                ExpenseCreatedDate = DateTime.Now,
                ExpenseCreatedBy = expenseVM.UserID,
                IsDeleted = false
            };
             _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
            expenseVM.ErrorMessage = string.Empty;
            expenseVM.SuccessMessage = "Expense is created successfully";
            return expenseVM;
        }

        public async Task<Expense> GetExpense(int expenseID)
        {
            return await _context.Expenses.FirstOrDefaultAsync(e => e.ExpenseID == expenseID && !e.IsDeleted);
        }

        public async Task<List<Expense>> GetAllExpense()
        {
            return await _context.Expenses.Where(e => !e.IsDeleted).AsNoTracking().ToListAsync();
        }

        public async Task<ExpenseVM> UpdateExpense(int expenseID, ExpenseVM expenseVM)
        {
            var expense = await _context.Expenses.FirstOrDefaultAsync(e => e.ExpenseID == expenseID && !e.IsDeleted);

            if (expense is null)
            {
                return null;
            }

            expense.ExpenseName = expenseVM.ExpenseName;
            expense.ExpenseDescription = expenseVM.ExpenseDescription;
            expense.ExpenseAmount = expenseVM.ExpenseAmount;
            expense.ExpenseUpdatedDate = DateTime.Now;
            expense.ExpenseUpdatedBy = expenseVM.UserID;

            _context.Expenses.Update(expense);
            await _context.SaveChangesAsync();
            expenseVM.ErrorMessage = string.Empty;
            expenseVM.SuccessMessage = "Expense is created successfully";
            return expenseVM;
        }

        public async Task<Expense> DeleteExpense(int expenseID)
        {
            var expense = await _context.Expenses.FirstOrDefaultAsync(e => e.ExpenseID == expenseID && !e.IsDeleted);

            if (expense == null)
            {
                return null;
            }

            expense.IsDeleted = true;
            expense.ExpenseDeletedDate = DateTime.Now;
            _context.Expenses.Update(expense);
            await _context.SaveChangesAsync();
            return expense;
        }


        //private void ValidateExpenseDetailsVM(ExpenseVM expenseVM)
        //{
           

        //    if (string.IsNullOrWhiteSpace(expenseVM.ExpenseName))
        //    {
        //        throw new ArgumentException("expenseName type cannot be empty or null", nameof(expenseVM.ExpenseName));
        //    }

        //}

        public async Task<List<Expense>> BulkDelete(List<int> expenseId)
        {
            var expenses = await _context.Expenses.Where(f => expenseId.Contains(f.ExpenseID) && (f.IsDeleted == null || f.IsDeleted == false)).AsNoTracking().ToListAsync();
            foreach (var expense in expenses)
            {
                expense.IsDeleted = true;
            }

            _context.Expenses.UpdateRange(expenses);
            await _context.SaveChangesAsync();
            return expenses;
        }

    }
}
