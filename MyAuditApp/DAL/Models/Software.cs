using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Software
    {
        public int SoftwareId { get; set; }
        public string SoftwareName { get; set; }
        public string SoftwareType { get; set; }
        public string SoftwareDescription { get; set; }
        public int MonthId { get; set; }
        public int YearId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
