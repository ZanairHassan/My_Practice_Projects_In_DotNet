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
    internal class InsuranceService : IInsuranceService
    {
        private readonly ApplicationDBContext _context;

        public InsuranceService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Insurance> CreateInsurance(InsuranceVM insuranceVM)
        {
            Insurance insurance = new Insurance();
            insurance.InsuranceCreatedDate = DateTime.Now;
            insurance.InsuranceDescription=insuranceVM.InsuranceDescription;
            insurance.InsuranceCreatedBy=insuranceVM.UserId;
            insurance.InsuranceName=insuranceVM.InsuranceName;
            insurance.InsuranceType=insuranceVM.InsuranceType;
            await _context.Insurances.AddAsync(insurance);
            await _context.SaveChangesAsync();
            return insurance;
        }

        public async Task<Insurance> DeleteInsurance(int insuranceId)
        {
            var insurance = await _context.Insurances.FirstOrDefaultAsync(x => x.InsuranceId == insuranceId);
            if (insurance != null)
            {
                insurance.InsuranceDeletedDate = DateTime.Now;
                insurance.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
            return insurance;
        }

        public async Task<List<Insurance>> GetAllInsurances()
        {
            return await _context.Insurances.ToListAsync();
        }

        public async Task<Insurance> GetInsurance(int insuranceId)
        {
            return await _context.Insurances.FirstOrDefaultAsync(x => x.InsuranceId == insuranceId);
        }

        public async Task<Insurance> UpdateInsurance(int insuranceId, InsuranceVM insuranceVM)
        {
            var insurance = await _context.Insurances.FirstOrDefaultAsync(x => x.InsuranceId == insuranceId);
            if(insurance != null)
            {
                insurance.InsuranceUpdatedDate = DateTime.Now;
                insurance.InsuranceDescription= insuranceVM.InsuranceDescription;
                insurance.InsuranceUpdatedBy=insuranceVM.UserId;
                insurance.InsuranceName=insuranceVM.InsuranceName;
                insurance.InsuranceType=insuranceVM.InsuranceType;
                _context.Insurances.Update(insurance);
                await _context.SaveChangesAsync();
            }
            return insurance;
        }
    }
}
