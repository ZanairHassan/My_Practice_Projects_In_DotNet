using BLL.Interfaces;
using DAL.DBContext;
using DAL.Models;
using DAL.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace BLL.Services
{
    public class MobileCompanyService : IMobileCompanyService
    {
        private readonly AuditAppDBContext _auditDBContext;
        public MobileCompanyService(AuditAppDBContext auditDBContext)
        {
            _auditDBContext = auditDBContext;
        }

        public async Task<MobileCompanyVM> CreateMobileCompany(MobileCompanyVM mobileCompanyVM)
        {
            if (mobileCompanyVM == null)
            {
                throw new ArgumentNullException(nameof(mobileCompanyVM), "mobileCompanyVM cannot be null");
            }
            MobileCompany mobileCompany = new MobileCompany();
            mobileCompany.MobileCompanyName = mobileCompanyVM.MobileCompanyName;
            mobileCompany.MobileCompanyEmail = mobileCompanyVM.MobileCompanyEmail;
            mobileCompany.MobileCompanyCity = mobileCompanyVM.MobileCompanyCity;
            mobileCompany.MobileCompanyCountry = mobileCompanyVM.MobileCompanyCountry;
            mobileCompany.MobileCompanyPhone = mobileCompanyVM.MobileCompanyPhone;
            mobileCompany.MobileCompanyRegion = mobileCompanyVM.MobileCompanyRegion;
            mobileCompany.MobileCompanyPostalCode = mobileCompanyVM.MobileCompanyPostalCode;
            await _auditDBContext.MobileCompanies.AddAsync(mobileCompany);
            await _auditDBContext.SaveChangesAsync();
            mobileCompanyVM.ErrorMessage = string.Empty;
            mobileCompanyVM.SuccessMessage = "Expense is created successfully";
            return mobileCompanyVM;
        }

        public async Task<MobileCompany> DeleteMobileCompany(int mobilecompanyID)
        {
            var mobilecompany = await _auditDBContext.MobileCompanies.FirstOrDefaultAsync(x => x.MobileCompanyID == mobilecompanyID);
            if (mobilecompany != null)
            {
                mobilecompany.IsDeleted = true;
                await _auditDBContext.SaveChangesAsync();
            }
            return mobilecompany;
        }

        public async Task<List<MobileCompany>> GetAllMobileCompany()
        {
            return await _auditDBContext.MobileCompanies.ToListAsync();
        }

        public async Task<MobileCompany> GetMobileCompany(int mobilecompanyID)
        {
            return await _auditDBContext.MobileCompanies.FirstOrDefaultAsync(x => x.MobileCompanyID == mobilecompanyID);
        }

        public async Task<MobileCompanyVM> UpdateMobileCompany(int mobilecompanyID, MobileCompanyVM mobilecompanyVM)
        {
            var mobilecompany = await _auditDBContext.MobileCompanies.FirstOrDefaultAsync(x => x.MobileCompanyID == mobilecompanyID);
            if (mobilecompany is null)
            {
                return null;
            }

            mobilecompany.MobileCompanyName = mobilecompanyVM.MobileCompanyName;
            mobilecompany.MobileCompanyEmail = mobilecompanyVM.MobileCompanyEmail;
            mobilecompany.MobileCompanyCity = mobilecompanyVM.MobileCompanyCity;
            mobilecompany.MobileCompanyCountry = mobilecompanyVM.MobileCompanyCountry;
            mobilecompany.MobileCompanyPhone = mobilecompanyVM.MobileCompanyPhone;
            mobilecompany.MobileCompanyRegion = mobilecompanyVM.MobileCompanyRegion;
            mobilecompany.MobileCompanyPostalCode = mobilecompanyVM.MobileCompanyPostalCode;
            _auditDBContext.MobileCompanies.Update(mobilecompany);
            await _auditDBContext.SaveChangesAsync();
            mobilecompanyVM.ErrorMessage = string.Empty;
            mobilecompanyVM.SuccessMessage = "Moble cmpny is created successfully";
            return mobilecompanyVM;
        }

        public async Task<List<MobileCompany>> BulkDelete(List<int> mobileCompanyId)
        {
            var mblcompany = await _auditDBContext.MobileCompanies.Where(f => mobileCompanyId.Contains(f.MobileCompanyID) && (f.IsDeleted == null || f.IsDeleted == false)).ToListAsync();
            foreach (var mbl in mblcompany)
            {
                mbl.IsDeleted = true;
            }

            _auditDBContext.MobileCompanies.UpdateRange(mblcompany);
            await _auditDBContext.SaveChangesAsync();
            return mblcompany;
        }

    }
}
