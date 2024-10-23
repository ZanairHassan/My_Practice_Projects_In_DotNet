using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Module
    {
        public int ModuleID { get; set; }
        public string? ModuleName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
