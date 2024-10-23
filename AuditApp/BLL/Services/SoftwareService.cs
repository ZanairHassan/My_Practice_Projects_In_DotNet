using BLL.Interfaces;
using DAL.DBContext;
using DAL.Models;
using DAL.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class SoftwareService : ISoftwareService
    {
        private readonly AuditAppDBContext _auditDBContext;
        public SoftwareService(AuditAppDBContext auditDBContext)
        {
            _auditDBContext = auditDBContext;
        }

        public async Task<SoftwareVM> CreateSoftware(SoftwareVM softwareVM)
        {
            if (softwareVM == null)
            {
                throw new ArgumentNullException(nameof(softwareVM), "softwareVM cannot be null");
            }
            Software software = new Software();
            software.SoftwareName = softwareVM.SoftwareName;
            software.SoftwareDescription = softwareVM.SoftwareDescription;
            software.SoftwareType = softwareVM.SoftwareType;
            await _auditDBContext.Softwares.AddAsync(software);
            await _auditDBContext.SaveChangesAsync();
            softwareVM.ErrorMessage = string.Empty;
            softwareVM.SuccessMessage = "Software is created successfully";
            return softwareVM;
        }

        public async Task<Software> DeleteSoftware(int softwareID)
        {
            var software = await _auditDBContext.Softwares.FirstOrDefaultAsync(x => x.SoftwareID == softwareID);
            if (software != null)
            {
                software.IsDeleted = true;
                await _auditDBContext.SaveChangesAsync();
            }
            return software;
        }

        public async Task<List<Software>> GetAllSoftware()
        {
            return await _auditDBContext.Softwares.AsNoTracking().ToListAsync();
        }

        public async Task<Software> GetSoftware(int softwareID)
        {
            return await _auditDBContext.Softwares.FirstOrDefaultAsync(x => x.SoftwareID == softwareID);
        }

        public async Task<SoftwareVM> UpdateSoftware(int softwareID, SoftwareVM softwareVM)
        {
            var software = await _auditDBContext.Softwares.FirstOrDefaultAsync(x => x.SoftwareID == softwareID);
            if(software is null)
            {
                return null;
            }
            software.SoftwareName = softwareVM.SoftwareName;
            software.SoftwareDescription = softwareVM.SoftwareDescription;
            software.SoftwareType = softwareVM.SoftwareType;
            _auditDBContext.Softwares.Update(software);
            await _auditDBContext.SaveChangesAsync();
            softwareVM.ErrorMessage = string.Empty;
            softwareVM.SuccessMessage = "software is created successfully";
            return softwareVM;
        }

        public async Task<List<Software>> BulkDelete(List<int> softwareId)
        {
            var softwares = await _auditDBContext.Softwares
                .Where(f => softwareId
                .Contains(f.SoftwareID) && (f.IsDeleted == null || f.IsDeleted == false))
                .AsNoTracking()
                .ToListAsync();
            foreach (var software in softwares)
            {
                software.IsDeleted = true;
            }

            _auditDBContext.Softwares.UpdateRange(softwares);
            await _auditDBContext.SaveChangesAsync();
            return softwares;
        }


    }
}
