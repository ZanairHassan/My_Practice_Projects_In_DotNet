using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class InvestmentType
    {
        public int InvestmentTypeId { get; set; }
        public string InvestmentTypeName { get; set; }
        public string InvestmentTypeDescription { get; set; }
        public int? InvestmentTypeIsCreatedBy { get; set; }
        public int? InvestmentTypeUpdatedBy { get; set; }
        public int? InvestmentTypeDeletedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsDeleted { get; set; }

    }
}
