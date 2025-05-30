﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Income
    {
        public int IncomeID { get; set; }
        public string IncomeDetails { get; set; }
        public string IncomeTypeName { get; set; }
        public int MonthId { get; set; }
        public int YearId { get; set; }
        public DateTime? IncomeCreatedDate { get; set; }
        public DateTime? IncomeUpdatedDate { get; set; }
        public DateTime? IncomeDeletedDate { get; set; }
        public int? IncomeCreatedBy { get; set; }
        public int? IncomeUpdatedBy { get; set; }
        public int? IncomeDeletedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
