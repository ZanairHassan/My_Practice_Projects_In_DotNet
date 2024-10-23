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

    public class InvestmentService : IInvestmentService
    { 

        private readonly AuditAppDBContext _auditAppDBContext;

        public InvestmentService(AuditAppDBContext auditAppDBContext)
        {
            _auditAppDBContext = auditAppDBContext;
        }

        public async Task<InvestmentVM> CreateInvestment(InvestmentVM investmentVM,int investmentTypeId)
        {
            var investmentType = _auditAppDBContext.InvestmentTypes.FirstOrDefault(x => x.InvestmentTypeId == investmentTypeId);
            if (investmentType == null)
            {
                throw new Exception($"investmentType with ID {investmentTypeId} not found.");
            }
            if (investmentVM == null)
            {
                throw new ArgumentNullException(nameof(investmentVM), "investmentVM cannot be null");
            }
            Investment investment = new Investment();
            investment.CreatedDate = DateTime.Now;
            investment.InvestmentName=investmentVM.InvestmentName;
            investment.InvestmentDetails= investmentType.InvestmentTypeName;
            investment.InvestmentCreatedBy=investmentVM.UserId;
            await _auditAppDBContext.Investments.AddAsync(investment);
            await _auditAppDBContext.SaveChangesAsync();
            investmentVM.ErrorMessage = string.Empty;
            investmentVM.SuccessMessage = "Investment is created successfully";
            return investmentVM;

        }

        public async Task<Investment> DeleteInvestmentById(int investmentId)
        {
            var investment=await _auditAppDBContext.Investments.FirstOrDefaultAsync(x=> x.InvestmentId==investmentId);
            if (investment != null)
            {
                investment.IsDeleted=true;
                investment.DeletedDate = DateTime.Now;
                await _auditAppDBContext.SaveChangesAsync();
            }
            return investment;
        }

        public async Task<List<Investment>> GetAllInvestMents()
        {
            return await _auditAppDBContext.Investments.AsNoTracking().ToListAsync();
        }

        public async Task<Investment> GetInvestmentById(int investmentId)
        {
            return await _auditAppDBContext.Investments.FirstOrDefaultAsync(x => x.InvestmentId == investmentId);
        }

        public async Task<InvestmentVM> UpdateInvestment(int investmentId, InvestmentVM investmentVM)
        {
            var investment=await _auditAppDBContext.Investments.FirstOrDefaultAsync(x=> x.InvestmentId==investmentId);
            if (investment is null)
            {
                return null;
            }
            investment.UpdatedTime = DateTime.Now;
            investment.InvestmentUpdatedBy = investmentVM.UserId;
            investment.InvestmentName = investmentVM.InvestmentName;
            _auditAppDBContext.Investments.Update(investment);
            await _auditAppDBContext.SaveChangesAsync();
            investmentVM.ErrorMessage = string.Empty;
            investmentVM.SuccessMessage = "Investment is Updated successfully";
            return investmentVM;

        }

        public async Task<List<Investment>> BulkDelete(List<int> investmentId)
        {
            var investments = await _auditAppDBContext.Investments
                .Where(f => investmentId
                .Contains(f.InvestmentId) && (f.IsDeleted == null || f.IsDeleted == false))
                .AsNoTracking()
                .ToListAsync();
            foreach (var investment in investments)
            {
                investment.IsDeleted = true;
            }

            _auditAppDBContext.Investments.UpdateRange(investments);
            await _auditAppDBContext.SaveChangesAsync();
            return investments;
        }

    }
}
