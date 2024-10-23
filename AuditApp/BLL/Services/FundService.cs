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
    public class FundService : IFundService
    {
        private readonly AuditAppDBContext _auditDBContext;
        public FundService(AuditAppDBContext auditDBContext)
        {
            _auditDBContext = auditDBContext;
        }
        public async Task<FundVM> CreateFund(FundVM fundVM,int fundTypeId)
        {
            var fundType=_auditDBContext.Fundtypes.FirstOrDefault(x=> x.FundTypeID==fundTypeId);
            if (fundType == null)
            {
                throw new Exception($"fundType with ID {fundTypeId} not found.");
            }
            if (fundVM == null)
            {
                throw new ArgumentNullException(nameof(fundVM), "fundVM cannot be null");
            }


            Fund fund = new Fund();
            fund.FundTypeCreatedDate = DateTime.Now;
            fund.FundName = fundVM.FundName;
            fund.FundTypeCreatedBy = fundVM.UserID;
            fund.FundTypeName=fundType.FundTypeName;
            await _auditDBContext.Funds.AddAsync(fund);
            await _auditDBContext.SaveChangesAsync();
            fundVM.ErrorMessage = string.Empty;
            fundVM.SuccessMessage = "Expense is created successfully";
            return fundVM;
        }

        public async Task<Fund> DeleteFund(int fundID)
        {
            var fund = await _auditDBContext.Funds.FirstOrDefaultAsync(x => x.FundID == fundID);
            if (fund != null)
            {
                fund.IsDeleted = true;
                fund.FundTypeDeletedDate = DateTime.Now;
                await _auditDBContext.SaveChangesAsync();
            }

            return fund;
        }

        public async Task<List<Fund>> GetAllFund()
        {
            return await _auditDBContext.Funds.AsNoTracking().ToListAsync();
        }

        public async Task<Fund> GetFund(int fundID)
        {
            return await _auditDBContext.Funds.FirstOrDefaultAsync(x => x.FundID == fundID);
        }

        public async Task<FundVM> UpdateFund(int fundID, FundVM fundVM)
        {
            var fund = await _auditDBContext.Funds.FirstOrDefaultAsync(x => x.FundID == fundID);
            if (fund is null)
            {
                return null;
            }
            fund.FundTypeUpdatedDate = DateTime.Now;
            fund.FundTypeUpdatedBy = fundVM.UserID;
            fund.FundName = fundVM.FundName;
            _auditDBContext.Funds.Update(fund);
            await _auditDBContext.SaveChangesAsync();
            fundVM.ErrorMessage = string.Empty;
            fundVM.SuccessMessage = "Expense is created successfully";
            return fundVM;
        }


        public async Task<List<Fund>> BulkDelete(List<int> fundId)
        {
            var funds = await _auditDBContext.Funds
                .Where(f => fundId
                .Contains(f.FundID) && (f.IsDeleted == null || f.IsDeleted == false))
                .AsNoTracking()
                .ToListAsync();
            foreach (var fund in funds)
            {
                fund.IsDeleted = true;
            }

            _auditDBContext.Funds.UpdateRange(funds);
            await _auditDBContext.SaveChangesAsync();
            return funds;
        }


    }
}
