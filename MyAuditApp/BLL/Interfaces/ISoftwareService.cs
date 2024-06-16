using DAL.Models;
using DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ISoftwareService
    {
        Task<Software> CreateSoftware(SoftwareVM softwareVM);
        Task<List<Software>> GetAllSoftwares();
        Task<Software> GetSoftware(int softwareId);
        Task<Software> UpdateSoftware(int softwareId, SoftwareVM softwareVM);
        Task<Software> DeleteSoftware(int softwareId);

    }
}
