using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Fund
    {
        public int FundID { get; set; }
        public string FundName { get; set; }
        //public int FundType { get; set; }
        public string FundTypeName { get; set; }
        public int MonthId { get; set; }
        public int YearId { get; set; }
        public DateTime? FundTypeCreatedDate { get; set; }
        public DateTime? FundTypeUpdatedDate { get; set; }
        public DateTime? FundTypeDeletedDate { get; set; }
        public int? FundTypeCreatedBy { get; set; }
        public int? FundTypeUpdatedBy { get; set; }
        public int? FundTypeDeletedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
