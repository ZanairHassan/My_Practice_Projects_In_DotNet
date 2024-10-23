using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels
{
    public class GuestUserVM:GenericVM
    {
        [Required(ErrorMessage = "GuestEmail is required")]
        [EmailAddress]
        public string GuestEmail { get; set; }
        [Required(ErrorMessage = "GuestFirstName is required")]
        public string GuestFirstName { get; set; }
        [Required(ErrorMessage = "GuestLastName is required")]
        public string GuestLastName { get; set; }
    }
}
