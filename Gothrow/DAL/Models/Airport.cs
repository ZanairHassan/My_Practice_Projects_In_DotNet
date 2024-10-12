using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Airport
    {
        public int AirportId { get; set; }
        public string AirportName { get; set; }
        public string AirportType { get; set; }
        public string AirportCountry { get; set; }
        public string AirportCity { get; set; }
        public DateTime? AirportCreatedDate { get; set; }
        public DateTime? AirportUpdatedDate { get;set;}
        public DateTime? AirportDeletedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
