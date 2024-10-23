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
    public class ExpenseSubTypeService:IExpenseSubTypeService
    {

        private readonly AuditAppDBContext _auditDBContext;
        public ExpenseSubTypeService(AuditAppDBContext auditDBContext)
        {
            _auditDBContext = auditDBContext;
        }

        public async Task<ExpenseSubType> CreateExpenseSubType(ExpenseSubTypeVM expensesubtypeVM)
        {
            ExpenseSubType expensesubtype = new ExpenseSubType();
            expensesubtype.ExpenseSubTypeDescription = expensesubtypeVM.ExpenseSubTypeDescription;
            expensesubtype.ExpenseSubTypeName = expensesubtypeVM.ExpenseSubTypeName;
            await _auditDBContext.ExpenseSubTypes.AddAsync(expensesubtype);
            await _auditDBContext.SaveChangesAsync();

            return expensesubtype;
        }
        public async Task<ExpenseSubType> DeleteExpenseSubType(int expensesubtypeID)
        {
            var expensesubtype = await _auditDBContext.ExpenseSubTypes.FirstOrDefaultAsync(x => x.ExpenseSubTypeId == expensesubtypeID);
            if (expensesubtype != null)
            {
                expensesubtype.Isdeleted = true;
                expensesubtype.ExpenseSubTypeDeletedDate = DateTime.Now;
                await _auditDBContext.SaveChangesAsync();
            }
            return expensesubtype;
        }

        public async Task<List<ExpenseSubType>> GetAllExpenseSubType()
        {
            return await _auditDBContext.ExpenseSubTypes.AsNoTracking().ToListAsync();
        }

        public async Task<ExpenseSubType> GetExpenseSubType(int expensesubtypeID)
        {
            return await _auditDBContext.ExpenseSubTypes.FirstOrDefaultAsync(x => x.ExpenseSubTypeId == expensesubtypeID);
        }

        public async Task<ExpenseSubType> UpdateExpenseSubType(int expensesubtypeID, ExpenseSubTypeVM expensesubtypeVM)
        {
            var expensesubtype = await _auditDBContext.ExpenseSubTypes.FirstOrDefaultAsync(x => x.ExpenseSubTypeId == expensesubtypeID);
            if (expensesubtype != null)
            {
                expensesubtype.ExpenseSubTypeDescription = expensesubtypeVM.ExpenseSubTypeDescription;
                expensesubtype.ExpenseSubTypeName = expensesubtypeVM.ExpenseSubTypeName;
                _auditDBContext.ExpenseSubTypes.Update(expensesubtype);
                await _auditDBContext.SaveChangesAsync();
            }
            return expensesubtype;
        }
    }
}
