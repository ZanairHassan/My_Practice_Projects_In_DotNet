using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ModulePermission
    {
        public int ModulePermissionId { get; set; }
        public int ModuleId { get; set; }
        public int UserId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
