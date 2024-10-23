using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class UserRequest
    {
        [Key]
        public int RequestID { get; set; }
        public int UserID { get; set; }
        public DateTime LastRequestTime { get; set; }
    }
}
