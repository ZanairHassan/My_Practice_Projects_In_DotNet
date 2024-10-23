using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels
{
    public class IncomeTypeVM : GenericVM
    {
        public int UserID { get; set; }
        public string IncomeTypeName { get; set; }
        public string IncomeTypeDescription { get; set; }
    }
}
