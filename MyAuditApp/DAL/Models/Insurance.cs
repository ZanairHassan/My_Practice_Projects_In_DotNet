using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Insurance
    {
        public int InsuranceId { get; set; }
        public string InsuranceName { get; set; }
        public string InsuranceDescription { get; set; }
        public string InsuranceType { get; set; }
        public int MonthId { get; set; }
        public int YearId { get; set; }
        public DateTime? InsuranceCreatedDate { get; set; }
        public DateTime? InsuranceUpdatedDate { get; set; }
        public DateTime? InsuranceDeletedDate { get; set; }
        public int InsuranceCreatedBy { get; set; }
        public int InsuranceUpdatedBy { get; set; }
        public int InsuranceDeletedBy { get; set; }
        public int IsDeleted { get; set; }
    }
}
