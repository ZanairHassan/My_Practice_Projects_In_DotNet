using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels
{
    public class ModulePermissionVM : GenericVM
    {
        public int ModuleID { get; set; }
        public int UserID { get; set; }
    }
}
