using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ExpenseSubType
    {
        public int ExpenseSubTypeId { get; set; }
        public string ExpenseSubTypeName { get; set; }
        public string ExpenseSubTypeDescription { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? ExpenseSubTypeDeletedDate { get; set; }
    }
}
