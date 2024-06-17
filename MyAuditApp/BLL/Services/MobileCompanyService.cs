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
    public class MobileCompanyService : IMobileCompanyService
    {
        private readonly ApplicationDBContext _context;

        public MobileCompanyService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<MobileCompany> CreateMobileCompany(MobileCompanyVM mobileCompanyVM)
        {
            MobileCompany mobileCompany = new MobileCompany();
            mobileCompany.MobileCompanyId = mobileCompanyVM.UserId;
            mobileCompany.MobileCompanyName = mobileCompanyVM.MobileCompanyName;
            mobileCompany.MobileCompanyEmail = mobileCompanyVM.MobileCompanyEmail;
            mobileCompany.MobileCompanyCity = mobileCompanyVM.MobileCompanyCity;
            mobileCompany.MobileCompanyCountry= mobileCompanyVM.MobileCompanyCountry;
            mobileCompany.MobileCompanyPhone= mobileCompanyVM.MobileCompanyPhone;
            mobileCompany.MobileCompanyRegion= mobileCompanyVM.MobileCompanyRegion;
            mobileCompany.MobileCompanyPostalCode= mobileCompanyVM.MobileCompanyPostalCode;
            await _context.MobileCompanies.AddAsync(mobileCompany);
            await _context.SaveChangesAsync();
            return mobileCompany;
        }

        public async Task<MobileCompany> DeleteMobileCompany(int mobileCompanyId)
        {
            var mobileCompany = await _context.MobileCompanies.FirstOrDefaultAsync(x => x.MobileCompanyId == mobileCompanyId);
            if (mobileCompany != null)
            {
                mobileCompany.IsDeleted=true;
                await _context.SaveChangesAsync();
            }
            return mobileCompany;
        }

        public async Task<List<MobileCompany>> GetAllMobileCompanies()
        {
            return await _context.MobileCompanies.ToListAsync();
        }

        public async Task<MobileCompany> GetMobileCompany(int mobileCompanyId)
        {
            return await _context.MobileCompanies.FirstOrDefaultAsync(x => x.MobileCompanyId == mobileCompanyId);
        }

        public async Task<MobileCompany> UpdateMobileCmpany(int mobileCompanyId, MobileCompanyVM mobileCompanyVM)
        {
            var mobileCompany = await _context.MobileCompanies.FirstOrDefaultAsync(x => x.MobileCompanyId == mobileCompanyId);
            if (mobileCompany != null)
            {
                mobileCompany.MobileCompanyName= mobileCompanyVM.MobileCompanyName;
                mobileCompany.MobileCompanyEmail= mobileCompanyVM.MobileCompanyEmail;
                mobileCompany.MobileCompanyCity= mobileCompanyVM.MobileCompanyCity;
                mobileCompany.MobileCompanyCountry= mobileCompanyVM.MobileCompanyCountry;
                mobileCompany.MobileCompanyPhone= mobileCompanyVM.MobileCompanyPhone;
                mobileCompany.MobileCompanyRegion= mobileCompanyVM.MobileCompanyRegion;
                mobileCompany.MobileCompanyPostalCode= mobileCompanyVM.MobileCompanyPostalCode;
                _context.MobileCompanies.Update(mobileCompany);
                await _context.SaveChangesAsync();
            }
            return mobileCompany;
        }
    }
}
