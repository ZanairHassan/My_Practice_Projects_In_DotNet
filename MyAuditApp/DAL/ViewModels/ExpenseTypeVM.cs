using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels
{
    public class ExpenseTypeVM
    {
        public int UserId { get; set; }
        public string ExpenseTypeDescription { get; set; }
        public string JwtToken { get; set; }
    }
}
