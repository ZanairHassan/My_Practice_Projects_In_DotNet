using DAL.Models;
using DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IFundTypeService
    {
        Task<FundType> CreateFundType(FundTypeVM fundTypeVM);
        Task<FundType> UpdateFundType(int fundTypeId, FundTypeVM fundTypeVM);
        Task<FundType> DeleteFundType(int fundTypeId);
        Task<FundType> GetFundType(int fundTypeId);
        Task<FundType> GetAllFundType();
    }
}
