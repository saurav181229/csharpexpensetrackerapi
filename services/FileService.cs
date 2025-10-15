using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using expenseTrackerapi.model;

namespace ExpenseTrackerAPI.services
{
    public class FileService: IfileSerivice
    {
        private string  _filePath = "/Users/saurav/Desktop/csharprefresher/ExpenseTrackerApi/Data/Expense.json";
        public List<Expense> LoadData()
        {
            if (File.Exists(_filePath))
            {
                var jsonData = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<Expense>>(jsonData) ?? new List<Expense>();
            }
            return new List<Expense>();
            
        }
        public void SaveData(List<Expense> expenses)
        {
            string path = Directory.GetDirectoryRoot(_filePath);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var jsonData = JsonSerializer.Serialize(expenses, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(_filePath, jsonData);

        }

    }
}