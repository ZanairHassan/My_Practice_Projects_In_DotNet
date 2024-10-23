using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Investment
    {
        public int InvestmentId { get; set; }
        public string InvestmentName { get; set; }
        public string InvestmentDetails { get; set; }
        public int InvestmentCreatedBy { get; set; }
        public int InvestmentUpdatedBy { get; set; }
        public int InvestmentDeletedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsDeleted { get; set; }

    }
}
