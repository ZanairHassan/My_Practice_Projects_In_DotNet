using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels
{
    public class FundTypeVM
    {
        public int UserId { get; set; }
        public string FundTypeName { get; set; }
        public string FundTypeDescription { get; set; }
        public string JwtToken { get; set; }
    }
}
