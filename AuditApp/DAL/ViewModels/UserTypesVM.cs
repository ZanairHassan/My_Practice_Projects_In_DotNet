using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels
{
    public class UserTypesVM : GenericVM
    {
        public int UserTypesID { get; set; }
        public string? UserType { get; set; }
    }
}
