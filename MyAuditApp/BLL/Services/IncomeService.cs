using BLL.Interfaces;
using DAL.DBContext;
using DAL.Models;
using DAL.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class IncomeService: IIncomeService
    {
        private readonly ApplicationDBContext _context;

        public IncomeService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Income> CreateIncome(IncomeVM incomeVM)
        {
           Income income = new Income();
            income.IncomeCreatedDate = DateTime.Now;
            income.IncomeTypeId = incomeVM.IncomeTypeId;
            await _context.Incomes.AddAsync(income);
            await _context.SaveChangesAsync();
            return income;
        }

        public async Task<Income> DeleteIncome(int incomeId)
        {
            var income=await _context.Incomes.FirstOrDefaultAsync(x=> x.IncomeId == incomeId);
            if (income != null)
            {
                income.IsDeleted = true;
                income.IncomeDeletedDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            return income;
        }

        public async Task<List<Income>> GetAllIncome()
        {
           return await _context.Incomes.ToListAsync();
        }

        public Task<Income> GetIncome(int incomeId)
        {
            return _context.Incomes.FirstOrDefaultAsync(x => x.IncomeId == incomeId);
        }

        public async Task<Income> UpdateIncome(int incomeId, IncomeVM incomeVM)
        {
           var income=await _context.Incomes.FirstOrDefaultAsync(x=> x.IncomeId == incomeId);
            if (income != null)
            {
                income.IncomeUpdatedDated = DateTime.Now;
                income.IncomeUpdatedBy=incomeVM.UserId;
                income.IncomeTypeId = incomeVM.IncomeTypeId;
                _context.Incomes.Update(income);
                await _context.SaveChangesAsync();
            }
            return income;
        }
    }
}
