using DAL.Models;
using DAL.ViewModels;

namespace BLL.Interfaces
{
    public interface IFundService
    {
        Task<FundVM> CreateFund(FundVM fundVM,int fundTypeId);
        Task<Fund> GetFund(int fundID);
        Task<List<Fund>> GetAllFund();
        Task<FundVM> UpdateFund(int fundID, FundVM fundVM);
        Task<Fund> DeleteFund(int fundID);
        Task<List<Fund>> BulkDelete(List<int> fundId);
    }
}
