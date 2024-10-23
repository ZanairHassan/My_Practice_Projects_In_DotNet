using DAL.Models;
using DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IInvestmentService
    {
        Task <InvestmentVM> CreateInvestment(InvestmentVM investmentVM,int investmentId);
        Task<Investment> GetInvestmentById(int investmentId);
        Task<Investment> DeleteInvestmentById(int investmentId);
        Task<List<Investment>> GetAllInvestMents();
        Task<InvestmentVM> UpdateInvestment(int investmentId, InvestmentVM investmentVM);
        Task<List<Investment>> BulkDelete(List<int> investmentId);
    }
}
