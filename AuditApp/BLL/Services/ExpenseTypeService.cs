using BLL.Interfaces;
using DAL.DBContext;
using DAL.Models;
using DAL.ViewModels;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ExpenseTypeService : IExpenseTypeService
    {
        private readonly AuditAppDBContext _auditDBContext;
        public ExpenseTypeService(AuditAppDBContext auditDBContext)
        {
            _auditDBContext = auditDBContext;
        }
        public async Task<ExpenseTypeVM> CreateExpenseType(ExpenseTypeVM expensetypeVM)
        {

            if (expensetypeVM == null)
            {
                throw new ArgumentNullException(nameof(expensetypeVM), "expenseVM cannot be null");
            }
            ValidateExpenseTypeDetailsVM(expensetypeVM);

            if (await IsExpenseTypeNameDuplicate(expensetypeVM.ExpenseTypeName))
            {
                throw new ArgumentException("ExpenseTypeName  already exists ", nameof(expensetypeVM.ExpenseTypeName));
            }


            ExpenseType expenseType = new ExpenseType();
            expenseType.ExpenseTypeName = expensetypeVM.ExpenseTypeName;
            _auditDBContext.ExpenseTypes.Add(expenseType);
            await _auditDBContext.SaveChangesAsync();
            expensetypeVM.ErrorMessage = string.Empty;
            expensetypeVM.SuccessMessage = "Expense type is created successfully";

            return expensetypeVM;
        }

        public async Task<ExpenseType> DeleteExpenseType(int expensetypeID)
        {
            var expensetype = await _auditDBContext.ExpenseTypes.FirstOrDefaultAsync(x => x.ExpenseTypeID == expensetypeID && !x.IsDeleted);
            if (expensetype == null)
            {
                return null;
            }
            expensetype.IsDeleted = true;
            expensetype.ExpenseTypeDeletedDate = DateTime.Now;
            _auditDBContext.Update(expensetype);
            await _auditDBContext.SaveChangesAsync();

            return expensetype;
        }

        public async Task<List<ExpenseType>> GetAllExpenseType()
        {
            return await _auditDBContext.ExpenseTypes.ToListAsync();
        }

        public async Task<ExpenseType> GetExpenseByExpenseTypeName(string expenseTypeName)
        {
            return await _auditDBContext.ExpenseTypes.FirstOrDefaultAsync(x => x.ExpenseTypeName == expenseTypeName);
        }

        public async Task<ExpenseType> GetExpenseType(int expensetypeID)
        {
            return await _auditDBContext.ExpenseTypes.FirstOrDefaultAsync(x => x.ExpenseTypeID == expensetypeID);
        }

        public async Task<ExpenseTypeVM> UpdateExpenseType(int expensetypeID, ExpenseTypeVM expensetypeVM)
        {
            var expensetype = await _auditDBContext.ExpenseTypes.FirstOrDefaultAsync(x => x.ExpenseTypeID == expensetypeID);
            if (expensetype != null)
            {
                expensetype.ExpenseTypeName = expensetypeVM.ExpenseTypeName;
                 _auditDBContext.ExpenseTypes.Update(expensetype);
                await _auditDBContext.SaveChangesAsync();
            }
            expensetypeVM.ErrorMessage = string.Empty;
            expensetypeVM.SuccessMessage = "Expense type is created successfully";

            return expensetypeVM;
        }


        public async Task<List<ExpenseType>> BulkDelete(List<int> expenseTypeId)
        {
            var expenseTypes = await _auditDBContext.ExpenseTypes.Where(f => expenseTypeId.Contains(f.ExpenseTypeID) && (f.IsDeleted == null || f.IsDeleted == false)).ToListAsync();
            foreach (var expense in expenseTypes)
            {
                expense.IsDeleted = true;
            }

            _auditDBContext.ExpenseTypes.UpdateRange(expenseTypes);
            await _auditDBContext.SaveChangesAsync();
            return expenseTypes;
        }

        private void ValidateExpenseTypeDetailsVM(ExpenseTypeVM expenseTypeVM)
        {


            if (string.IsNullOrWhiteSpace(expenseTypeVM.ExpenseTypeName))
            {
                throw new ArgumentException("expenseName type cannot be empty or null", nameof(expenseTypeVM.ExpenseTypeName));
            }

        }

        private async Task<bool> IsExpenseTypeNameDuplicate(string expenseTypeName)
        {
            return await _auditDBContext.ExpenseTypes.AnyAsync(x => x.ExpenseTypeName == expenseTypeName );
        }

    }
}
