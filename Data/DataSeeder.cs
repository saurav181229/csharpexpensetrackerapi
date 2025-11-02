using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTrackerAPI.model;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerAPI.Data
{
    public static class DataSeeder
    {
        public static async Task SeedUsersAndAssignExpenseAsync(ExpenseContext context)
        {
            
            
            var allUsers = await context.Users.ToListAsync();
        var allExpenses = await context.Expenses.ToListAsync();

        var random = new Random();
            foreach (var expense in allExpenses)
            {
                var randomUser = allUsers[random.Next(allUsers.Count)];
                expense.UserId = randomUser.Id;
            }
         await context.SaveChangesAsync();

        Console.WriteLine("✅ Assigned 50k expenses randomly among 5 users.");

        }
        
    }
}