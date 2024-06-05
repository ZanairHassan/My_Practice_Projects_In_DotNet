using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class UserTypes
    {
        public int UserTypeid { get; set; }
        public string? UserType { get; set; }
        public bool Isdeleted { get; set; }
    }
}
