using DAL.Models;
using DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IIncomeTypeService
    {
        Task<IncomeType> CreateIncomeType(IncomeTypeVM incomeTypeVM);
        Task<IncomeType> UpdateIncomeType(int incomeTypeId, IncomeTypeVM incomeTypeVM);
        Task<IncomeType> GetIncomeType(int incomeTypeId);
        Task<List<IncomeType>> GetAllIncomeType();
        Task<IncomeType> DeleteIncomeType(int incomeTypeId);
    }
}
