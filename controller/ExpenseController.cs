using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using expenseTrackerapi.model;
using ExpenseTrackerAPI.DTO;
using ExpenseTrackerAPI.model;
using ExpenseTrackerAPI.services;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet("getExpenses")]
        public ActionResult<List<Expense>> GetExpenses()
        {
            // var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var expenses = _expenseService.DisplayExpenses();
            return Ok(expenses);
        }

        [HttpPost("addExpense")]
        public ActionResult AddExpense([FromBody]ExpenseDto expense)
        {
            bool isAdded = _expenseService.AddExpense(expense);
            if (!isAdded)
            return StatusCode(500, "Failed to add expense.");

            return Ok(new { message = "Expense added successfully." });
        }

        [HttpDelete("deleteExpense/{id}")]
        public ActionResult DeleteExpense(int id)
        {
            int result = _expenseService.DeleteExpense(id);
            if (result == 1) return StatusCode(200, "Deleted successfully");
            else return Ok(new { messsage = "Unable to delete" });
        }

        [HttpGet("viewTotalExpenses")]
        public ActionResult ViewTotalExpenses()
        {
            decimal expense = _expenseService.ViewTotalExpense();
            return Ok(expense);
        }




        [HttpPut("UpdateExpense")]
        public ActionResult UpdateExpense(ExpenseDto expense)
        {
            int result = _expenseService.UpdateExpense(expense);
            if (result == 1) return StatusCode(200, "Updated successfully");
            else return Ok(new { messsage = "Unable to Update" });

        }

   [HttpGet("SpendByCategory")]
        public ActionResult SpendByCategory()
        {
            List<SpendByCategory> spends = _expenseService.SpendingByCategory();
            return Ok(spends);
        }



    }
}