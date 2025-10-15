using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using expenseTrackerapi.model;
using ExpenseTrackerAPI.model;

namespace ExpenseTrackerAPI.services
{
    public class ExpenseService: IExpenseService
    {
        private IfileSerivice _fileService;
        private List<Expense> _expenses;
        public ExpenseService(IfileSerivice fileService)
        {
            _fileService = fileService;
            _expenses = _fileService.LoadData();
        }

        public List<Expense> DisplayExpenses()
        {
            if (_expenses is not null && _expenses.Count > 0)
            {
                foreach (var expense in _expenses)
                {
                    Console.WriteLine($"ID: {expense.Id}, Amount: {expense.amount}, Category: {expense.Category}, Description: {expense.Description}");
                }
            }
            else
            {
                Console.WriteLine("No expenses to display.");
            }
            return _expenses;
        }

        public bool AddExpense(Expense expense)
        {
            
            expense.Id = SetNextId();
            _expenses.Add(expense);
            _fileService.SaveData(_expenses);
            Console.WriteLine("Expense added successfully.");
            return true;
        }
        
        public Expense DeleteExpense(int id)
        {
            var expense = _expenses.FirstOrDefault(e => e.Id == id);
            _expenses.Remove(expense);
            if (expense == null)
            {
                Console.WriteLine($"Expense with ID {id} not found.");
                return new Expense();
            }
            foreach(Expense exp in _expenses)
            {
                if(exp.Id > id)
                {
                    exp.Id -= 1;
                }
            }
            _fileService.SaveData(_expenses);
            return expense;
        }

        public int ViewTotalExpense()
        {
            return _expenses.Sum(e => e.amount);
        }
        public List<SpendByCategory> SpendingByCategory()
        {
            var catGroup = _expenses.GroupBy(e => e.Category)
            .Select(g => new { Category = g.Key, TotalAmount = g.Sum(e => e.amount) });
            return catGroup.Select(c => new SpendByCategory
            {
                Category = c.Category,
                TotalAmount = c.TotalAmount
            }).ToList();
        }
        private int SetNextId()
        {
            return _expenses.Any() ? _expenses.Max(e => e.Id) +1: 1;
            
        }
    }
}