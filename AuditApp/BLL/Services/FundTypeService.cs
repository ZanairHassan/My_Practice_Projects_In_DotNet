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
        private readonly AuditAppDBContext _auditDBContext;
        public FundTypeService(AuditAppDBContext auditDBContext)
        {
            _auditDBContext = auditDBContext;
        }

        public async Task<FundTypeVM> CreateFundType(FundTypeVM fundtypeVM)
        {

            if (fundtypeVM == null)
            {
                throw new ArgumentNullException(nameof(fundtypeVM), "fundtypeVM cannot be null");
            }
            ValidateFundTypeDetailsVM(fundtypeVM);

            if (await IsFundTypeNameDuplicate(fundtypeVM.FundTypeName))
            {
                throw new ArgumentException("FundTypeName  already exists ", nameof(fundtypeVM.FundTypeName));
            }

            FundType fundtype = new FundType();
            fundtype.FundTypeName = fundtypeVM.FundTypeName;
            fundtype.FundTypeDescription = fundtypeVM.FundTypeDescription;
            await _auditDBContext.Fundtypes.AddAsync(fundtype);
            await _auditDBContext.SaveChangesAsync();
            fundtypeVM.ErrorMessage = string.Empty;
            fundtypeVM.SuccessMessage = "Expense is created successfully";
            return fundtypeVM;
        }

        public async Task<FundType> DeleteFundType(int fundtypeID)
        {
            var fundtype = await _auditDBContext.Fundtypes.FirstOrDefaultAsync(x => x.FundTypeID == fundtypeID);
            if (fundtype != null)
            {
                fundtype.IsDeleted = true;
                fundtype.FundTypeDeletedDate = DateTime.Now;
                await _auditDBContext.SaveChangesAsync();
            }

            return fundtype;
        }

        public async Task<List<FundType>> GetAllFundType()
        {
            return await _auditDBContext.Fundtypes.ToListAsync();
        }

        public async Task<FundType> GetFundType(int fundtypeID)
        {
            return await _auditDBContext.Fundtypes.FirstOrDefaultAsync(x => x.FundTypeID == fundtypeID);
        }

        public async Task<FundType> GetFundTypeByName(string FundTypeName)
        {
            return await _auditDBContext.Fundtypes.FirstOrDefaultAsync(x=> x.FundTypeName==FundTypeName);
        }

        public async Task<FundTypeVM> UpdateFundType(int fundtypeID, FundTypeVM fundtypeVM)
        {
            var fundtype = await _auditDBContext.Fundtypes.FirstOrDefaultAsync(x => x.FundTypeID == fundtypeID);
            if (fundtype is null)
            {
                return null;
            }
            fundtype.FundTypeName = fundtypeVM.FundTypeName;
            fundtype.FundTypeDescription = fundtypeVM.FundTypeDescription;
            _auditDBContext.Fundtypes.Update(fundtype);
            await _auditDBContext.SaveChangesAsync();
            fundtypeVM.ErrorMessage = string.Empty;
            fundtypeVM.SuccessMessage = "Expense is created successfully";
            return fundtypeVM;
        }


        public async Task<List<FundType>> BulkDelete(List<int> fundTypeId)
        {
            var fundTypes = await _auditDBContext.Fundtypes.Where(f => fundTypeId.Contains(f.FundTypeID) && (f.IsDeleted == null || f.IsDeleted == false)).ToListAsync();
            foreach (var fund in fundTypes)
            {
                fund.IsDeleted = true;
            }

            _auditDBContext.Fundtypes.UpdateRange(fundTypes);
            await _auditDBContext.SaveChangesAsync();
            return fundTypes;
        }

        private void ValidateFundTypeDetailsVM(FundTypeVM fundTypeVM)
        {


            if (string.IsNullOrWhiteSpace(fundTypeVM.FundTypeName))
            {
                throw new ArgumentException("FundTypeName type cannot be empty or null", nameof(fundTypeVM.FundTypeName));
            }

        }

        private async Task<bool> IsFundTypeNameDuplicate(string fundTypeName)
        {
            return await _auditDBContext.Fundtypes.AnyAsync(x => x.FundTypeName == fundTypeName);
        }

    }
}
