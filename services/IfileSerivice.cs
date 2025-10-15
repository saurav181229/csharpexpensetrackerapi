using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using expenseTrackerapi.model;

namespace ExpenseTrackerAPI.services
{
    public interface IfileSerivice
    {
        public List<Expense> LoadData();
        public void SaveData(List<Expense> expenses);
    }
}