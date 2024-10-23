using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Request
    {
        public int RequestId { get; set; }
        public DateTime LastRequestTime { get; set; }
        public int UserId { get; set; }
    } 
}
