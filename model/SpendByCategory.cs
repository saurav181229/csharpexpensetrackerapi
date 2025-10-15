using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTrackerAPI.model
{
    public class SpendByCategory
    {
        public string Category { get; set; }
        public int TotalAmount { get; set; }
    }
}