using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace expenseTrackerapi.model
{
    public class Expense
    {
  
     
         public  int Id  { get;   set; }
   
    public string Category { get; set; }

    public string Description { get; set; }

    public int amount { get; set; }
    }
}