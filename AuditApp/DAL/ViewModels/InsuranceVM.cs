﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels
{
    public class InsuranceVM : GenericVM
    {
        public int UserID { get; set; }
        public string InsuranceName { get; set; }
        public string InsuranceDescription { get; set; }
        public string InsuracneType { get; set; }
    }
}
