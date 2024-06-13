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
    public class FundService:IFundService
    {
        private readonly ApplicationDBContext _context;
        public FundService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Fund> CreateFund(FundVM fundVM)
        {
            Fund fund = new Fund();
            fund.FundTypeCreatedDate = DateTime.Now;
            fund.FundTypeCreatedBy=fundVM.UserId;
            await _context.Funds.AddAsync(fund);
            await _context.SaveChangesAsync();
            return fund;
        }

        public async Task<Fund> DeleteFund(int fundId)
        {
            var fund=await _context.Funds.FirstOrDefaultAsync(x=> x.FundId==fundId);
            if (fund != null)
            {
                fund.IsDeleted=true;
                fund.FundTypeDeletedDate= DateTime.Now;
                await _context.SaveChangesAsync();
            }
            return fund;
        }

        public async Task<List<Fund>> GetAllFunds()
        {
            return await _context.Funds.ToListAsync();
        }

        public Task<Fund> GetFund(int fundId)
        {
            return _context.Funds.FirstOrDefaultAsync(x=> x.FundId==fundId);
        }

        public async Task<Fund> UpdateFund(int fundId, FundVM fundVM)
        {
            var fund=await _context.Funds.FirstOrDefaultAsync(x=> x.FundId==fundId);
            if (fund != null)
            {
                fund.FundTypeUpdatedDate= DateTime.Now;
                fund.FundTypeUpdatedBy=fundVM.UserId;
                _context.Funds.Update(fund);
                await _context.SaveChangesAsync();
            }
            return fund;
        }
    }
}
