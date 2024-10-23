using DAL.Models;
using DAL.ViewModels;

namespace BLL.Interfaces
{
    public interface IFundTypeService
    {
        Task<FundTypeVM> CreateFundType(FundTypeVM fundtypeVM);
        Task<FundType> GetFundType(int fundtypeID);
        Task<List<FundType>> GetAllFundType();
        Task<FundTypeVM> UpdateFundType(int fundtypeID, FundTypeVM fundtypeVM);
        Task<FundType> DeleteFundType(int fundtypeID);
        Task<FundType> GetFundTypeByName(string FundtypeName);
        Task<List<FundType>> BulkDelete(List<int> fundTypeId);
    }
}
