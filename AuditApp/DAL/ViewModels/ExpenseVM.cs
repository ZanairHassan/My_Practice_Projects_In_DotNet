using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels
{
    public class ExpenseVM : GenericVM
    {
        public string? ExpenseName { get; set; }
        public string? ExpenseDescription { get; set; }
        public decimal ExpenseAmount { get; set; }
        //public string JwtToken { get; set; }
        public int UserID { get; set; }
    }
}
