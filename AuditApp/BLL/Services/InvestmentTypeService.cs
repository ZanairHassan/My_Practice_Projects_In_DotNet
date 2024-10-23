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
    public class InvestmentTypeService : IInvestmentTypeService
    {
        private readonly AuditAppDBContext _auditAppDBContext;

        public InvestmentTypeService(AuditAppDBContext auditAppDBContext)
        {
            _auditAppDBContext = auditAppDBContext;
        }

        public async Task<InvestmentTypeVM> CreateInvestmentType(InvestmentTypeVM investmentTypeVM)
        {
            if (investmentTypeVM == null)
            {
                throw new ArgumentNullException(nameof(investmentTypeVM), "investmentTypeVM cannot be null");
            }
            ValidateInvestmentTypeDetailsVM(investmentTypeVM);

            if (await IsInvestmentTypeNameDuplicate(investmentTypeVM.InvestmentTypeName))
            {
                throw new ArgumentException("InvestmentTypeName  already exists ", nameof(investmentTypeVM.InvestmentTypeName));
            }

            InvestmentType investmentType =new InvestmentType();
            investmentType.InvestmentTypeName=investmentTypeVM.InvestmentTypeName;
            investmentType.InvestmentTypeIsCreatedBy = investmentTypeVM.UserId;
            investmentType.InvestmentTypeDescription=investmentTypeVM.InvestmentTypeDescription;
            investmentType.CreatedDate=DateTime.Now;
            await _auditAppDBContext.AddAsync(investmentType);
            await _auditAppDBContext.SaveChangesAsync();
            investmentTypeVM.ErrorMessage = string.Empty;
            investmentTypeVM.SuccessMessage = "Expense is created successfully";
            return investmentTypeVM;
        }

        public async Task<InvestmentType> DeleteInvestmentType(int investmentTypeId)
        {
            var investmentType = await _auditAppDBContext.InvestmentTypes.FirstOrDefaultAsync(x => x.InvestmentTypeId == investmentTypeId);
            if (investmentType != null)
            {
                investmentType.IsDeleted = true;
                await _auditAppDBContext.SaveChangesAsync();
            }
            return investmentType;
        }

        public async Task<List<InvestmentType>> GetAllInvestmentTypes()
        {
            return await _auditAppDBContext.InvestmentTypes.AsNoTracking().ToListAsync();
        }

        public async Task<InvestmentType> GetInvestmentType(int investmentTypeId)
        {
            return await _auditAppDBContext.InvestmentTypes.FirstOrDefaultAsync(x => x.InvestmentTypeId == investmentTypeId);
        }

        public async Task<InvestmentTypeVM> UpdateInvestMentType(InvestmentTypeVM investmentTypeVM, int investmentTypeId)
        {
            var investmentType=await _auditAppDBContext.InvestmentTypes.FirstOrDefaultAsync(x=> x.InvestmentTypeId==investmentTypeId);
            if (investmentType is null)
            {
                return null;
            }
            investmentType.UpdatedTime = DateTime.Now;
            investmentType.InvestmentTypeName = investmentTypeVM.InvestmentTypeName;
            investmentType.InvestmentTypeUpdatedBy = investmentTypeVM.UserId;
            investmentType.InvestmentTypeDescription = investmentTypeVM.InvestmentTypeDescription;
            _auditAppDBContext.InvestmentTypes.Update(investmentType);
            await _auditAppDBContext.SaveChangesAsync();
            investmentTypeVM.ErrorMessage = string.Empty;
            investmentTypeVM.SuccessMessage = "InvestmentType is created successfully";
            return investmentTypeVM;
        }

        public async Task<List<InvestmentType>> BulkDelete(List<int> investTypeId)
        {
            var investTypes = await _auditAppDBContext.InvestmentTypes
                .Where(f => investTypeId
                .Contains(f.InvestmentTypeId) && (f.IsDeleted == null || f.IsDeleted == false))
                .AsNoTracking()
                .ToListAsync();
            foreach (var investTyp in investTypes)
            {
                investTyp.IsDeleted = true;
            }

            _auditAppDBContext.InvestmentTypes.UpdateRange(investTypes);
            await _auditAppDBContext.SaveChangesAsync();
            return investTypes;
        }

        private void ValidateInvestmentTypeDetailsVM(InvestmentTypeVM investTypeVM)
        {
            if (string.IsNullOrWhiteSpace(investTypeVM.InvestmentTypeName))
            {
                throw new ArgumentException("InvestmentTypeName type cannot be empty or null", nameof(investTypeVM.InvestmentTypeName));
            }
        }

        private async Task<bool> IsInvestmentTypeNameDuplicate(string investTypeName)
        {
            return await _auditAppDBContext.InvestmentTypes.AnyAsync(x => x.InvestmentTypeName == investTypeName);
        }

    }
}
