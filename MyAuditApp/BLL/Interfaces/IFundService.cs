using DAL.Models;
using DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IFundService
    {
        Task<Fund> CreateFund(FundVM fundVM);
        Task<Fund> UpdateFund(int fundId,FundVM fundVM);
        Task<Fund> DeleteFund(int fundId);
        Task<Fund> GetFund(int fundId);
        Task<List<Fund>> GetAllFunds();
    }
}
