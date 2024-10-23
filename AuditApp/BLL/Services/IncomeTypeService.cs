using BLL.Interfaces;
using DAL.DBContext;
using DAL.Models;
using DAL.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace BLL.Services
{
    public class IncomeTypeService : IIncomeTypeService
    {

        private readonly AuditAppDBContext _auditDBContext;

        public IncomeTypeService(AuditAppDBContext auditdbContext)
        {
            _auditDBContext = auditdbContext;
        }
        public async Task<IncomeTypeVM> CreateIncomeType(IncomeTypeVM incometypeVM)
        {
            if (incometypeVM == null)
            {
                throw new ArgumentNullException(nameof(incometypeVM), "incometypeVM cannot be null");
            }
            ValidateIncomeTypeDetailsVM(incometypeVM);

            if (await IsIncomeTypeNameDuplicate(incometypeVM.IncomeTypeName))
            {
                throw new ArgumentException("IncomeTypeName  already exists ", nameof(incometypeVM.IncomeTypeName));
            }

            IncomeType incomeType = new IncomeType();
            //incomeType.IncomeTypeID = incometypeVM.UserID;
            incomeType.IncomeTypeName = incometypeVM.IncomeTypeName;
            incomeType.IncomeTypeDescription = incometypeVM.IncomeTypeDescription;
            await _auditDBContext.IncomeTypes.AddAsync(incomeType);
            await _auditDBContext.SaveChangesAsync();
            incometypeVM.ErrorMessage = string.Empty;
            incometypeVM.SuccessMessage = "Expense is created successfully";
            return incometypeVM;
        }

        public async Task<IncomeType> DeleteIncomeType(int incometypeID)
        {
            var incometype = await _auditDBContext.IncomeTypes.FirstOrDefaultAsync(x => x.IncomeTypeID == incometypeID);
            if (incometype != null)
            {
                incometype.IsDeleted = true;
                // incometype.IncomeTypeDeletedDate = DateTime.Now;
                await _auditDBContext.SaveChangesAsync();
            }
            return incometype;
        }

        public async Task<List<IncomeType>> GetAllIncomeType()
        {
            return await _auditDBContext.IncomeTypes.ToListAsync();
        }

        public async Task<IncomeType> GetIncomeType(int incometypeID)
        {
            return await _auditDBContext.IncomeTypes.FirstOrDefaultAsync(x => x.IncomeTypeID == incometypeID);
        }

        public async Task<IncomeType> GetIncomeTypebyName(string IncomeTName)
        {
            return await _auditDBContext.IncomeTypes.FirstOrDefaultAsync(x => x.IncomeTypeName == IncomeTName);
        }

        public async Task<IncomeTypeVM> UpdateIncomeType(int incometypeID, IncomeTypeVM incometypeVM)
        {
            var incometype = await _auditDBContext.IncomeTypes.FirstOrDefaultAsync(x => x.IncomeTypeID == incometypeID);
            if (incometype is null)
            {
                return null;
            }
            incometype.IncomeTypeName = incometypeVM.IncomeTypeName;
            incometype.IncomeTypeDescription = incometypeVM.IncomeTypeDescription;
            _auditDBContext.IncomeTypes.Update(incometype);
            await _auditDBContext.SaveChangesAsync();
            incometypeVM.ErrorMessage = string.Empty;
            incometypeVM.SuccessMessage = "Expense is created successfully";
            return incometypeVM;
        }

        public async Task<List<IncomeType>> BulkDelete(List<int> incometypeID)
        {
            var incomeTypes = await _auditDBContext.IncomeTypes.Where(f => incometypeID.Contains(f.IncomeTypeID) && (f.IsDeleted == null || f.IsDeleted == false)).ToListAsync();
            foreach (var income in incomeTypes)
            {
                income.IsDeleted = true;
            }

            _auditDBContext.IncomeTypes.UpdateRange(incomeTypes);
            await _auditDBContext.SaveChangesAsync();
            return incomeTypes;
        }

        private void ValidateIncomeTypeDetailsVM(IncomeTypeVM incomeTypeVM)
        {


            if (string.IsNullOrWhiteSpace(incomeTypeVM.IncomeTypeName))
            {
                throw new ArgumentException("IncomeTypeName type cannot be empty or null", nameof(incomeTypeVM.IncomeTypeName));
            }

        }

        private async Task<bool> IsIncomeTypeNameDuplicate(string incomeTypeName)
        {
            return await _auditDBContext.IncomeTypes.AnyAsync(x => x.IncomeTypeName == incomeTypeName);
        }

    }
}
