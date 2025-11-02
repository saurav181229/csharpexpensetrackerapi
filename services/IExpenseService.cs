using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using expenseTrackerapi.model;
using ExpenseTrackerAPI.DTO;
using ExpenseTrackerAPI.model;

namespace ExpenseTrackerAPI.services
{
    public interface IExpenseService
    {
        public bool AddExpense(ExpenseDto expense);
      

        public List<Expense> DisplayExpenses();
       

        public int DeleteExpense(int id);
       
    
        public decimal ViewTotalExpense();


        public List<SpendByCategory> SpendingByCategory();

        public int UpdateExpense(ExpenseDto expense);
        


       

      

        // public void UpdateExpenses(int id);
       
       


    }
}