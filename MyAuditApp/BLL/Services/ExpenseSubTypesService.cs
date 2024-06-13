using System;
using DAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL.DBContext;
using DAL.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class ExpenseSubTypesService:IExpenseSubTypeService
    {
        private readonly ApplicationDBContext _context;
        public ExpenseSubTypesService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<ExpenseSubType> CreateExpenseSubType(ExpenseSubTypeVM expenseSubTypeVM)
        {
            var expenseSubType = new ExpenseSubType();
            expenseSubType.ExpenseSubTypeDescription= expenseSubTypeVM.ExpenseSubTypeDescription;
            expenseSubType.ExpenseSubTypeName= expenseSubTypeVM.ExpenseSubTypeName;
            await _context.ExpenseSubTypes.AddAsync(expenseSubType);
            await _context.SaveChangesAsync();
            return expenseSubType;
        }

        public async Task<ExpenseSubType> DeleteExpenseSubType(int expenseSubTypeId)
        {
            var expenseSubType = await _context.ExpenseSubTypes.FirstOrDefaultAsync(x => x.ExpenseSubTypeId == expenseSubTypeId);
            if (expenseSubType != null)
            {
                expenseSubType.IsDeleted=true;
                expenseSubType.ExpenseSubTypeDeletedDate= DateTime.Now;
                await _context.SaveChangesAsync();
            }
            return expenseSubType;
        }

        public async Task<List<ExpenseSubType>> GetAllExpenseSubtype()
        {
            return await _context.ExpenseSubTypes.ToListAsync();
        }

        public async Task<ExpenseSubType> GetExpenseSubType(int expenseSubTypeId)
        {
            return await _context.ExpenseSubTypes.FirstOrDefaultAsync(x => x.ExpenseSubTypeId == expenseSubTypeId);
        }

        public async Task<ExpenseSubType> UpdateExpenseSubType(int expenseSubTypeId, ExpenseSubTypeVM expenseSubTypeVM)
        {
            var expenseSubType = await _context.ExpenseSubTypes.FirstOrDefaultAsync(x => x.ExpenseSubTypeId == expenseSubTypeId);
            if (expenseSubType != null)
            {
                expenseSubType.ExpenseSubTypeDescription = expenseSubTypeVM.ExpenseSubTypeDescription;
                expenseSubType.ExpenseSubTypeName=expenseSubTypeVM.ExpenseSubTypeName;
                _context.ExpenseSubTypes.Update(expenseSubType);
                await _context.SaveChangesAsync();
            }
            return expenseSubType;

        }
    }
}
