﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Configuration
    {
        public int ConfigurationId { get; set; }
        [Required]
        public string ConfigurationKey { get; set; }
        public string ConfigurationValue { get; set; }
        public bool IsExpired { get; set; }
    }
}
