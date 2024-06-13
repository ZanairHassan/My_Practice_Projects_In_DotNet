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
    public class FundTypeService: IFundTypeService
    {
        private readonly ApplicationDBContext _context;

        public FundTypeService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<FundType> CreateFundType(FundTypeVM fundTypeVM)
        {
            FundType fundType = new FundType();
            fundType.FundTypeName = fundTypeVM.FundTypeName;
            fundType.FundTypeDescription = fundTypeVM.FundTypeDescription;
            await _context.FundTypes.AddAsync(fundType);
            await _context.SaveChangesAsync();
            return fundType;
        }

        public async Task<FundType> DeleteFundType(int fundTypeId)
        {
            var fundType=await _context.FundTypes.FirstOrDefaultAsync(x=> x.FundTypeId == fundTypeId);
            if (fundType != null)
            {
                fundType.IsDeleted = true;
                fundType.FundTypeDeletedDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            return fundType;
        }

        public async Task<List<FundType>> GetAllFundType()
        {
           return await _context.FundTypes.ToListAsync();
        }

        public async Task<FundType> GetFundType(int fundTypeId)
        {
            return await _context.FundTypes.FirstOrDefaultAsync(x=> x.FundTypeId==fundTypeId);
        }

        public async Task<FundType> UpdateFundType(int fundTypeId, FundTypeVM fundTypeVM)
        {
            var fundType=await _context.FundTypes.FirstOrDefaultAsync(x=> x.FundTypeId==fundTypeId);
            if (fundType != null)
            {
                fundType.FundTypeName=fundTypeVM.FundTypeName;
                fundType.FundTypeDescription=fundTypeVM.FundTypeDescription;
                _context.FundTypes.Update(fundType);
                await _context.SaveChangesAsync();
            }
            return fundType;
        }
    }
}
