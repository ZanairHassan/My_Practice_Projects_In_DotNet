using DAL.Models;
using DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IIncomeService
    {
        Task<Income> CreateIncome(IncomeVM incomeVM);
        Task<Income> UpdateIncome(int incomeId,IncomeVM incomeVM);
        Task<Income> DeleteIncome(int incomeId);
        Task<Income> GetIncome(int incomeId);
        Task<List<Income>> GetAllIncome();
    }
}
