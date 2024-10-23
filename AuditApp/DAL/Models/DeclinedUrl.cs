using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class DeclinedUrl
    {
        [Key]
        public int DeclinedID { get; set; }
        public string IPAddress { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
