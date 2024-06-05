using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Token
    {
        public int TokenId { get; set; }
        public string JwtToken { get; set; }
        public int UserId { get; set; }
        public DateTime? Createddate { get; set; }
        public bool IsExpired { get; set; }
        public bool IsDeleted { get; set; }
    }
}
