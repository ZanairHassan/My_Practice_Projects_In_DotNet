using DAL.Models;
using DAL.ViewModels;

namespace BLL.Interfaces
{
    public interface IInsuranceService
    {
        Task<InsuranceVM> CreateInsurance(InsuranceVM insuranceVM);
        Task<Insurance> GetInsurance(int insuranceID);
        Task<List<Insurance>> GetAllInsurance();
        Task<InsuranceVM> UpdateInsurance(int insuranceID, InsuranceVM insuranceVM);
        Task<Insurance> DeleteInsurance(int insuranceID);
        Task<List<Insurance>> BulkDelete(List<int> insuranceId);
    }
}
