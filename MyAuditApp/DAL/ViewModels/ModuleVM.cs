﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels
{
    public class ModuleVM
    {
        public int UserId { get; set; }
        public string? ModuleName { get; set; }
        public string JwtToken { get; set; }
    }
}
