using BLL.Interfaces;
using DAL.DBContext;
using DAL.Models;
using DAL.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace BLL.Services
{
    public class InsuranceService : IInsuranceService
    {

        private readonly AuditAppDBContext _auditDBContext;
        public InsuranceService(AuditAppDBContext dbContext)
        {
            _auditDBContext = dbContext;
        }

        public async Task<InsuranceVM> CreateInsurance(InsuranceVM insuranceVM)
        {
            Insurance insurance = new Insurance();
            insurance.InsuranceCreatedDate = DateTime.Now;
            insurance.InsuranceDescription = insuranceVM.InsuranceDescription;
            insurance.InsuranceCreatedBy = insuranceVM.UserID;
            insurance.InsuranceName = insuranceVM.InsuranceName;
            insurance.InsuracneType = insuranceVM.InsuracneType;
            await _auditDBContext.Insurances.AddAsync(insurance);
            await _auditDBContext.SaveChangesAsync();
            insuranceVM.ErrorMessage = string.Empty;
            insuranceVM.SuccessMessage = "Insurance is created successfully";
            return insuranceVM;
        }

        public async Task<Insurance> DeleteInsurance(int insuranceID)
        {
            var insurance = await _auditDBContext.Insurances.FirstOrDefaultAsync(x => x.InsuranceID == insuranceID);
            if (insurance != null)
            {
                insurance.IsDeleted = true;
                insurance.InsuranceDeletedDate = DateTime.Now;
                await _auditDBContext.SaveChangesAsync();
            }

            return insurance;
        }

        public async Task<List<Insurance>> GetAllInsurance()
        {
            return await _auditDBContext.Insurances.AsNoTracking().ToListAsync();
        }

        public async Task<Insurance> GetInsurance(int insuranceID)
        {
            return await _auditDBContext.Insurances.FirstOrDefaultAsync(x => x.InsuranceID == insuranceID);
        }

        public async Task<InsuranceVM> UpdateInsurance(int insuranceID, InsuranceVM insuranceVM)
        {
            var insurance = await _auditDBContext.Insurances.FirstOrDefaultAsync(x => x.InsuranceID == insuranceID);
            if (insurance is null)
            {
                return null;
            }
            insurance.InsuranceUpdatedDate = DateTime.Now;
            insurance.InsuranceDescription = insuranceVM.InsuranceDescription;
            insurance.InsuranceUpdatedBy = insuranceVM.UserID;
            insurance.InsuranceName = insuranceVM.InsuranceName;
            insurance.InsuracneType = insuranceVM.InsuracneType;
            _auditDBContext.Insurances.Update(insurance);
            await _auditDBContext.SaveChangesAsync();
            insuranceVM.ErrorMessage = string.Empty;
            insuranceVM.SuccessMessage = "Insurance is created successfully";
            return insuranceVM;

        }

        public async Task<List<Insurance>> BulkDelete(List<int> insuranceId)
        {
            var insurances = await _auditDBContext.Insurances
                .Where(f => insuranceId
                .Contains(f.InsuranceID) && (f.IsDeleted == null || f.IsDeleted == false))
                .AsNoTracking()
                .ToListAsync();
            foreach (var insurance in insurances)
            {
                insurance.IsDeleted = true;
            }
            _auditDBContext.Insurances.UpdateRange(insurances);
            await _auditDBContext.SaveChangesAsync();
            return insurances;
        }

    }
}
