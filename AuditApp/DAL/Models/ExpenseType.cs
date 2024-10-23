using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ExpenseType
    {
        public int ExpenseTypeID { get; set; }
        public string ExpenseTypeName { get; set; }
        public DateTime? ExpenseTypeDeletedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
