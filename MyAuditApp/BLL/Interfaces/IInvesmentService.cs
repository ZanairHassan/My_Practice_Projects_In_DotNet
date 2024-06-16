using DAL.Models;
using DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.InterfacesW
{
    public interface IInvesmentService
    {
        Task<Investment> CreateInvesment(InvestmentVM investmentVM);
        Task<Investment> UpdateInvesment(int InvestmentId, InvestmentVM investmentVM);
        Task<List<Investment>> GetAllInvestments();
        Task<Investment> GetInvestment(int investmentId);
        Task<Investment> DeleteInvestment(int investmentId);
    }
}
