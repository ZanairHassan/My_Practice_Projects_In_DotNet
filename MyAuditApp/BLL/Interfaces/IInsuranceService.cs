using DAL.Models;
using DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IInsuranceService
    {
        Task<Insurance> CreateInsurance(InsuranceVM insuranceVM);
        Task<Insurance> DeleteInsurance(int insuranceId);
        Task<List<Insurance>> GetAllInsurances();
        Task<Insurance> GetInsurance(int insuranceId);
        Task<Insurance> UpdateInsurance(int insuranceId, InsuranceVM insuranceVM);

    }
}
