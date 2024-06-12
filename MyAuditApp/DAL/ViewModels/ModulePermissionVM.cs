using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels
{
    public class ModulePermissionVM
    {
        public int ModuleId { get; set; }
        public int UserId { get; set; }
        public string JwtToken { get; set; }
    }
}
