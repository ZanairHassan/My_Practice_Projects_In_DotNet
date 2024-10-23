using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ModulePermission
    {
        public int ModulePermissionID { get; set; }
        public int ModuleID { get; set; }
        public int UserID { get; set; }
        public bool IsDeleted { get; set; }
    }
}
