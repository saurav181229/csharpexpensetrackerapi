using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTrackerAPI.model;

namespace ExpenseTrackerAPI.services
{
    public interface IAuthService
    {
        public abstract void CreatePasswordHash(string password, out byte[] hash, out byte[] salt);

        public  abstract bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);

        public  Task<bool> RegisterUser(string username, string password);
        public  Task<User?> ValidateUser(string username, string password);
        // async Task<bool> RegisterUser(string username, string password);
    }
}