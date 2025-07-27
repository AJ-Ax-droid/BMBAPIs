using BankingWebAPI.BLL.Interface;
using BankingWebAPI.DAL;
using BankingWebAPI.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingWebAPI.BLL.Repository
{
    public class UserLoginRepository : IUserLoginRepository
    {
        private readonly BankingDbContext _context;
        public UserLoginRepository(BankingDbContext context)
        {
            _context = context;
        }

        public Task<bool> ChangePasswordAsync(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public async Task<User> LoginAsync(string username, string password)
        {
            // Assuming you have a method to validate user credentials
            var user = _context.UserLoginDetail.FirstOrDefault(u => u.UserName == username && u.Password == password);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid username or password.");
            }
            var userDetails = await _context.Users.FirstOrDefaultAsync(u => u.UserID == user.UserID);
            return userDetails;



        }

        public Task<bool> ResetPasswordAsync(string username, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}
