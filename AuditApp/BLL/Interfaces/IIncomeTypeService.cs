using DAL.Models;
using DAL.ViewModels;

namespace BLL.Interfaces
{
    public interface IIncomeTypeService
    {
        Task<IncomeTypeVM> CreateIncomeType(IncomeTypeVM incometypeVM);
        Task<IncomeType> GetIncomeType(int incometypeID);
        Task<List<IncomeType>> GetAllIncomeType();
        Task<IncomeTypeVM> UpdateIncomeType(int incometypeID, IncomeTypeVM incometypeVM);
        Task<IncomeType> DeleteIncomeType(int incometypeID);
        Task<IncomeType> GetIncomeTypebyName(string IncomeTName);
        Task<List<IncomeType>> BulkDelete(List<int> incometypeID);
    }
}
