using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels
{
    public class IncomeVM : GenericVM
    {
        public string IncomeDetails { get; set; }
        public int UserID { get; set; }
    }
}
