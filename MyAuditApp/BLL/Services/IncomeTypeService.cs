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
    public class IncomeTypeService: IIncomeTypeService
    {
        private readonly ApplicationDBContext _context;

        public IncomeTypeService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IncomeType> CreateIncomeType(IncomeTypeVM incomeTypeVM)
        {
            IncomeType incomeType = new IncomeType();
            incomeType.IncomeTypeName = incomeTypeVM.IncomeTypeName;
            incomeType.IncomeTypeDescription= incomeTypeVM.IncomeTypeDescription;
            await _context.IncomeTypes.AddAsync(incomeType);
            await _context.SaveChangesAsync();
            return incomeType;
        }

        public async Task<IncomeType> DeleteIncomeType(int incomeTypeId)
        {
            var incomeType = await _context.IncomeTypes.FirstOrDefaultAsync(x => x.IncomeTypeId == incomeTypeId);
            if (incomeType != null)
            {
                incomeType.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
            return incomeType;
        }

        public async Task<List<IncomeType>> GetAllIncomeType()
        {
            return await _context.IncomeTypes.ToListAsync();
        }

        public Task<IncomeType> GetIncomeType(int incomeTypeId)
        {
           return _context.IncomeTypes.FirstOrDefaultAsync(x=> x.IncomeTypeId == incomeTypeId);
        }

        public async Task<IncomeType> UpdateIncomeType(int incomeTypeId, IncomeTypeVM incomeTypeVM)
        {
            var incomeType=await _context.IncomeTypes.FirstOrDefaultAsync(x=> x.IncomeTypeId == incomeTypeId);
            if (incomeType != null)
            {
                incomeType.IncomeTypeName = incomeTypeVM.IncomeTypeName;
                incomeType.IncomeTypeDescription= incomeTypeVM.IncomeTypeDescription;
                _context.IncomeTypes.Update(incomeType);
                await _context.SaveChangesAsync();
            }
            return incomeType;
        }
    }
}
