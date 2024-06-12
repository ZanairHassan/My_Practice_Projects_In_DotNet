using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels
{
    public class ExpenseSubTypeVM
    {
        public int UserId { get; set; }
        public string ExpenseSubTypeName { get; set; }
        public string ExpenseSubTypeDescription { get; set; }
        public string JwtToken { get; set; }
    }
}
