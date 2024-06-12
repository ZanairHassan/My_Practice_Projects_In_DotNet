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
        public string InvestMentName { get; set; }
        public string InvestmentDetails { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public bool Investmentdeleted { get; set; }
    }
}
