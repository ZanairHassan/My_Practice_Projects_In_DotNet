using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels
{
    public class SoftwareVM : GenericVM
    {
        public string SoftwareName { get; set; }
        public string SoftwareType { get; set; }
        public string SoftwareDescription { get; set; }
    }
}
