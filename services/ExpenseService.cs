using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using expenseTrackerapi.model;
using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.DTO;
using ExpenseTrackerAPI.model;

namespace ExpenseTrackerAPI.services
{
    public class ExpenseService: IExpenseService
    {
        // private IfileSerivice _fileService;
        private readonly ExpenseContext _context;
        private List<Expense> _expenses;
  
        private readonly int _UserId;
        public ExpenseService(ExpenseContext Expensecont,IUserContextService UserContextService)
        {
            _context = Expensecont;
            _UserId = UserContextService.GetUserId();
            // _expenses = _fileService.LoadData();
        }

        public List<Expense> DisplayExpenses()
        {
           
            return _context.DisplayExpenses(_UserId); ;
          
        }

        public bool AddExpense(ExpenseDto expense)
        {

            _context.AddExpense(expense,_UserId);
            return true;
        }
        
        public int DeleteExpense(int id)
        {
            int result = _context.DeleteExpense(id,_UserId);
            
            
            return result;
        }

        public decimal ViewTotalExpense()
        {
            return _context.GetTotalExpense(_UserId);
        }
        public List<SpendByCategory>  SpendingByCategory()
        {
            
            return _context.SpendByCategory(_UserId);
            
        }
        private int SetNextId()
        {
            return _expenses.Any() ? _expenses.Max(e => e.Id) + 1 : 1;

        }
        
        public int UpdateExpense(ExpenseDto expense)
        {
            int res = _context.UpdateExpense(expense,_UserId);

            return res;
        }
    }
}