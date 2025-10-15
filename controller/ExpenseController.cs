using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using expenseTrackerapi.model;
using ExpenseTrackerAPI.model;
using ExpenseTrackerAPI.services;
using Microsoft.AspNetCore.Mvc;

namespace expenseTrackerapi.controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpGet("getExpenses")]
        public ActionResult<List<Expense>> GetExpenses()
        {
            var expenses = _expenseService.DisplayExpenses();
            return Ok(expenses);
        }

        [HttpPost("addExpense")]
        public ActionResult AddExpense(Expense expense)
        {
            bool newExpense = _expenseService.AddExpense(expense);
            return Ok(newExpense);
        }

        [HttpGet("deleteExpense/{id}")]
        public ActionResult DeleteExpense(int id)
        {
            Expense expense = _expenseService.DeleteExpense(id);
            return Ok(expense);
        }

        [HttpGet("viewTotalExpenses")]
        public ActionResult ViewTotalExpenses()
        {
            int expense = _expenseService.ViewTotalExpense();
            return Ok(expense);
        }


        [HttpGet("spendingByCategory")]
        public ActionResult SpendingByCategory()
        {
            List<SpendByCategory> exp =  _expenseService.SpendingByCategory();
            return Ok(exp);
        }



    }
}