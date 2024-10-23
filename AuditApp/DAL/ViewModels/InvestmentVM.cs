using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels
{
    public class InvestmentVM : GenericVM
    {
        public int UserId { get; set; }
        public string InvestmentName { get; set; }
    }
}
