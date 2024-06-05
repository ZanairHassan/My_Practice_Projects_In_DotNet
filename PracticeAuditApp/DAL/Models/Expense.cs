using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Expense
    {
        public int ExpenseId { get; set; }
        public string ExpenseName { get; set; }
        public string ExpenseDescription { get; set; }
        public decimal ExpenseAmmount { get; set; }
        public int ExpenseTypeId { get; set; }
        public int ExpenseSubTypeId { get; set; }
        public int MonthId { get; set; }
        public int YearId { get; set; }
        public DateTime? ExpenseCreatedDate { get; set; }
        public DateTime? ExpenseUpdatedDate { get;set; }
        public DateTime? ExpenseDeletedDate { get; set; }
        public int ExpenseCreatedBy { get; set; }
        public int ExpenseUpdatedBy { get; set; }
        public int ExpenseDeletedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
