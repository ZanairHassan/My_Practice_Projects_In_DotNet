using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class IncomeType
    {
        public int IncomeTypeID { get; set; }
        public string IncomeTypeName { get; set; }
        public string IncomeTypeDescription { get; set; }
        public bool IsDeleted { get; set; }
    }
}
