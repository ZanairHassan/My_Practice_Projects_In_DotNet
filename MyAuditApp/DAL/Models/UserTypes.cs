using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class UserTypes
    {
        [Key]
        public int UserTypeId { get; set; }
        public string? UserType { get; set; }
        public bool Isdeleted { get; set; }
    }
}
