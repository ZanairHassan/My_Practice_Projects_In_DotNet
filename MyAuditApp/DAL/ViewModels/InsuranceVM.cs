using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels
{
    public class InsuranceVM
    {
        public int UserId { get; set; }
        public string InsuranceName { get; set; }
        public string InsuranceDescription { get; set; }
        public string InsuranceType { get; set; }
        public string JwtToken { get; set; }
    }
}
