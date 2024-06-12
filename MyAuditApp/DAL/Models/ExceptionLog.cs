﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ExceptionLog
    {
        [Key]
        public int ExceptionId { get; set; }
        public string Logtext { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}