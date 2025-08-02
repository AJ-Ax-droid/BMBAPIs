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
    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly BankingDbContext _context;
        public UserAccountRepository(BankingDbContext context)
        {
            _context = context;
        }
        public async Task<UserAccountDetail> GetUSerAccountDetailsByUserIDRepositoryAsync(int userID)
        {
            try
            {
                // Check if the userID is valid
                if (userID <= 0)
                {
                    throw new ArgumentException("Invalid user ID.");
                }

                var userAccountDetails = await _context.UserAccountDetails
                .FirstOrDefaultAsync(ua => ua.UserID == userID);
                if (userAccountDetails == null)
                {
                    throw new KeyNotFoundException($"User account details not found for user ID: {userID}");
                }
                return userAccountDetails;
                // Fetch the user account details by user ID
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while fetching user account details.", ex);

            }
        }
    }
}
