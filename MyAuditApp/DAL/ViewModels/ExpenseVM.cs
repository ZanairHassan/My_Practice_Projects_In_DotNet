using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels
{
    public class ExpenseVM
    {
        public string ExpenseName { get; set; }
        public string ExpenseDescription { get; set; }
        public decimal ExpenseAmmount { get; set; }
        public int ExpenseTypeId { get; set; }
        public int ExpenseSubTypedId { get; set; }
        public string JwtToken { get; set; }
        public int UserId { get; set; }
    }
}
