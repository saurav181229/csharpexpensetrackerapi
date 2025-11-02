using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTrackerAPI.DTO
{
    public class ExpenseDto
    {
              public DateTime Date { get; set; }
    public string Category { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public int Id { get; set; }
   
    }
}