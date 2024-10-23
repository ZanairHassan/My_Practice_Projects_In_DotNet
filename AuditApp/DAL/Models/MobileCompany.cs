using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class MobileCompany
    {
        public int MobileCompanyID { get; set; }
        public string MobileCompanyName { get; set; }
        public string MobileCompanyEmail { get; set; }
        public string MobileCompanyPhone { get; set; }
        public string MobileCompanyCity { get; set; }
        public string MobileCompanyRegion { get; set; }
        public string MobileCompanyPostalCode { get; set; }
        public string MobileCompanyCountry { get; set; }
        public int MonthId { get; set; }
        public int YearId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
