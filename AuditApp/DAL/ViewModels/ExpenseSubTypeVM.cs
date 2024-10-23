using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels
{
    public class ExpenseSubTypeVM: GenericVM
    {
        public int UserID { get; set; }
        public string ExpenseSubTypeName { get; set; }
        public string ExpenseSubTypeDescription { get; set; }
    }
}
