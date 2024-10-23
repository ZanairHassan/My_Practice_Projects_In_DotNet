using DAL.Models;
using DAL.ViewModels;

namespace BLL.Interfaces
{
    public interface IIncomeService
    {
        Task<IncomeVM> CreateIncome(IncomeVM incomeVM, int incomeTypeId);
        Task<Income> GetIncome(int incomeID);
        Task<List<Income>> GetAllIncome();
        Task<IncomeVM> UpdateIncome(int incomeID, IncomeVM incomeVM);
        Task<Income> DeleteIncome(int incomeID);
        Task<List<Income>> BulkDelete(List<int> incomeId);
    }
}
