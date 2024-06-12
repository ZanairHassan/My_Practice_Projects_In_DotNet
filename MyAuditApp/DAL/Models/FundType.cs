using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class FundType
    {
        public int FundTypeId { get; set; }
        public string FundTypeName { get; set; }
        public string FundTypeDescription { get; set; }
        public DateTime? FundTypeDeletedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
