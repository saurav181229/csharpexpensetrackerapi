using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using expenseTrackerapi.model;
using ExpenseTrackerAPI.model;

namespace ExpenseTrackerAPI.services
{
    public interface IExpenseService
    {
        public bool AddExpense(Expense expense);
      

        public List<Expense> DisplayExpenses();
       

        public Expense DeleteExpense(int id);
       
    
        public int ViewTotalExpense();
       

        public List<SpendByCategory>  SpendingByCategory();


       

      

        // public void UpdateExpenses(int id);
       
       


    }
}