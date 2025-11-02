using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.model;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerAPI.services
{
    public class AuthService:IAuthService
    {
        private readonly ExpenseContext _expenseContext;

        public AuthService(ExpenseContext expenseContext)
        {
            _expenseContext = expenseContext;
        }

        public async Task<bool> RegisterUser(string username, string password)
        {
            if (await _expenseContext.Users.AnyAsync(x => x.Username == username))
            {
                return false;
            }

            CreatePasswordHash(password, out byte[] hash, out byte[] salt);

            var user = new User
            {
                Username = username,
                PasswordHash = hash,
                PasswordSalt = salt


            };
            _expenseContext.Users.Add(user);
            await _expenseContext.SaveChangesAsync();
            return true;


        }

        public async Task<User?> ValidateUser(string username, string password)
        {
            var user = _expenseContext.Users.FirstOrDefault(x => x.Username == username);
            if (user is null)
            {
                return null;
            }
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        public  void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
        {
            using var hmac = new HMACSHA512();
            salt = hmac.Key;
            hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        public  bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using var hmac = new HMACSHA512(storedSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(storedHash);
        }
    }
}