using DAL.Models;
using DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IInvestmentTypeService
    {
        Task<InvestmentTypeVM> CreateInvestmentType(InvestmentTypeVM investmentTypeVM);
        Task<InvestmentType> GetInvestmentType(int investmentTypeId);
        Task<InvestmentType> DeleteInvestmentType(int investmentTypeId);
        Task<List<InvestmentType>> GetAllInvestmentTypes();
        Task<List<InvestmentType>> BulkDelete(List<int> investTypeId);
        Task<InvestmentTypeVM> UpdateInvestMentType(InvestmentTypeVM investmentTypeVM, int investmentTypeId);
    }
}
