using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ExceptionLogs
    {
        public int ExceptioId { get; set; }
        public string Logtext { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
