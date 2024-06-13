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
    public class ExpenseTypeService : IExpenseTypeService
    {
        private readonly ApplicationDBContext _context;
        public ExpenseTypeService(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<ExpenseType> CreateExpenseType(ExpenseTypeVM expenseTypeVM)
        {
           ExpenseType expenseType=new ExpenseType();
            expenseType.ExpenseTypeName = expenseTypeVM.ExpenseTypeName;
            _context.ExpenseTypes.Add(expenseType);
            await _context.SaveChangesAsync();
            return expenseType;
        }

        public async Task<ExpenseType> DeleteExpenseType(int expenseTypeId)
        {
           var expenseType=await _context.ExpenseTypes.FirstOrDefaultAsync(x=>x.ExpenseTypeId==expenseTypeId);
            if (expenseType == null)
            {
                expenseType.IsDeleted=true;
                expenseType.ExpenseTypeDeletedDate= DateTime.Now;
                await _context.SaveChangesAsync();
            }
            return expenseType;
        }

        public async Task<List<ExpenseType>> GetAllExpenseType()
        {
            return await _context.ExpenseTypes.ToListAsync();
        }

        public async Task<ExpenseType> GetExpenseType(int expenetTypeId)
        {
            return await _context.ExpenseTypes.FirstOrDefaultAsync(x => x.ExpenseTypeId == expenetTypeId);
        }

        public async Task<ExpenseType> GetExpenseTypeByName(string expenseTypeName)
        {
            return await _context.ExpenseTypes.FirstOrDefaultAsync(x => x.ExpenseTypeName == expenseTypeName);
        }

        public async Task<ExpenseType> UpdateExpenseType(int expenseTypeId, ExpenseTypeVM expenseTypeVM)
        {
            var expensetype=await _context.ExpenseTypes.FirstOrDefaultAsync(x=> x.ExpenseTypeId == expenseTypeId);
            if (expensetype != null)
            {
                expensetype.ExpenseTypeName=expenseTypeVM.ExpenseTypeName;
                _context.ExpenseTypes.Update(expensetype);
                await _context.SaveChangesAsync();
            }
            return expensetype;
        }
    }
}
