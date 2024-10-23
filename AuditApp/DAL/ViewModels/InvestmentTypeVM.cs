using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels
{
    public class InvestmentTypeVM : GenericVM
    {
        public string InvestmentTypeName { get; set; }
        public string InvestmentTypeDescription { get; set; }
        public int? UserId { get; set; }
    }
}
