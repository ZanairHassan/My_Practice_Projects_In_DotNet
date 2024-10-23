using BLL.Interfaces;
using DAL.DBContext;
using DAL.Models;
using DAL.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class IncomeService : IIncomeService
    {

        private readonly AuditAppDBContext _auditDBContext;
        public IncomeService(AuditAppDBContext auditdbContext)
        {
            _auditDBContext = auditdbContext;
        }

        public async Task<IncomeVM> CreateIncome(IncomeVM incomeVM,int incomeTypeId)
        {
            var incomeType = _auditDBContext.IncomeTypes.FirstOrDefault(x => x.IncomeTypeID == incomeTypeId);
            if (incomeType == null)
            {
                throw new Exception($"IncomeTypes with ID {incomeTypeId} not found.");
            }
            if (incomeVM == null)
            {
                throw new ArgumentNullException(nameof(incomeVM), "incomeVM cannot be null");
            }


            Income income = new Income();
            income.IncomeCreatedDate = DateTime.Now;
            income.IncomeDetails = incomeVM.IncomeDetails;
            income.IncomeCreatedBy=incomeVM.UserID;
            income.IncomeTypeName = incomeType.IncomeTypeName;
            await _auditDBContext.Incomes.AddAsync(income);
            await _auditDBContext.SaveChangesAsync();
            incomeVM.ErrorMessage = string.Empty;
            incomeVM.SuccessMessage = "Income is created successfully";
            return incomeVM;
        }

        public async Task<Income> DeleteIncome(int incomeID)
        {
            var income = await _auditDBContext.Incomes.FirstOrDefaultAsync(x => x.IncomeID == incomeID);
            if (income != null)
            {
                income.IsDeleted = true;
                income.IncomeDeletedDate = DateTime.Now;

                _auditDBContext.Incomes.Update(income);
                await _auditDBContext.SaveChangesAsync();
            }

            return income;
        }

        public async Task<List<Income>> GetAllIncome()
        {
            return await _auditDBContext.Incomes.AsNoTracking().ToListAsync();
        }

        public async Task<Income> GetIncome(int incomeID)
        {
            return await _auditDBContext.Incomes.FirstOrDefaultAsync(x => x.IncomeID == incomeID);
        }

        public async Task<IncomeVM> UpdateIncome(int incomeID, IncomeVM incomeVM)
        {
            var income = await _auditDBContext.Incomes.FirstOrDefaultAsync(x => x.IncomeID == incomeID);
            if (income != null)
            {
                income.IncomeUpdatedDate = DateTime.Now;
                income.IncomeDetails = incomeVM.IncomeDetails;
                income.IncomeUpdatedBy=incomeVM.UserID;
                _auditDBContext.Incomes.Update(income);
                await _auditDBContext.SaveChangesAsync();
                incomeVM.ErrorMessage = string.Empty;
                incomeVM.SuccessMessage = "Income is created successfully";
                return incomeVM;
            }
            return incomeVM;
        }

        public async Task<List<Income>> BulkDelete(List<int> incomeId)
        {
            var incomes = await _auditDBContext.Incomes
                .Where(f => incomeId
                .Contains(f.IncomeID) && (f.IsDeleted == null || f.IsDeleted == false))
                .AsNoTracking()
                .ToListAsync();
            foreach (var income in incomes)
            {
                income.IsDeleted = true;
            }
            _auditDBContext.Incomes.UpdateRange(incomes);
            await _auditDBContext.SaveChangesAsync();
            return incomes;
        }

    }
}
