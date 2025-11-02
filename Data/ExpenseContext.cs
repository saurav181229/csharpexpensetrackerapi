using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using expenseTrackerapi.model;
using ExpenseTrackerAPI.DTO;
using ExpenseTrackerAPI.model;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerAPI.Data
{
    public class ExpenseContext:DbContext
    {
        public ExpenseContext(DbContextOptions<ExpenseContext> options)
        : base(options)
        {


        }
     public DbSet<Expense> Expenses { get; set; }
     public DbSet<User> Users { get; set; }


     public List<Expense> DisplayExpenses(int UserId)
           {
            return Expenses.Where(x=>x.UserId==UserId).Take(10).ToList();
        } 

        public int AddExpense(ExpenseDto expenseDto,int UserId)
        {

            Expense expense = new Expense()
            {
                Amount = expenseDto.Amount,
                Category = expenseDto.Category,
                Description = expenseDto.Description,
                Date = expenseDto.Date,
                UserId= UserId
            };
            if (expense == null)
            {
                throw new ArgumentNullException(nameof(expense));
            }

            
            Expenses.Add(expense);
            int result = SaveChanges();

            return result;

        }
        public int DeleteExpense(int id,int UserId)
        {
            var expense = Expenses.FirstOrDefault(x => x.Id == id);

            if (expense == null)
            {
                return 0;
            }
            if (expense.UserId == UserId)
            {
                Expenses.Remove(expense);
                int result = SaveChanges();
                return result;
            }
            return 0;
           
        }

        public int UpdateExpense(ExpenseDto expense,int UserId)
        {
            var ExpenseToBeUpdate = Expenses.Where(x=>x.UserId==UserId).FirstOrDefault(x => x.Id == expense.Id);
            if (expense is not null)
            {

                ExpenseToBeUpdate.Amount = expense.Amount == 0 ? ExpenseToBeUpdate.Amount : expense.Amount;
                ExpenseToBeUpdate.Description = !string.IsNullOrEmpty(expense.Description) ? expense.Description : ExpenseToBeUpdate.Description;
                ExpenseToBeUpdate.Category = !string.IsNullOrEmpty(expense.Category) ? expense.Category : ExpenseToBeUpdate.Category;
               
            }
            int result = SaveChanges();
            return result;
        }

        public List<SpendByCategory> SpendByCategory(int UserId)
        {
            var catGroup = Expenses.Where(x=>x.UserId==UserId).GroupBy(ex => ex.Category).Select(g => new { Category = g.Key, TotalAmount = g.Sum(e => e.Amount) });
            return catGroup.Select(c => new SpendByCategory
            {
                Category = c.Category,
                TotalAmount = c.TotalAmount
            }).ToList();

        }

        public decimal GetTotalExpense(int UserId)
        {
            return Expenses.Where(x=>x.UserId==UserId).Sum(x => x.Amount);
        }
        
    }
}