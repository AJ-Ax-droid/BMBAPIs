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

        public async Task<APIResponseHandler<long>> GetUserAccountBalanceByAccountNoAndUserIdRepositoryAsync(int userID, string accountNo)
        {
            try
            {
                if (userID <= 0 || string.IsNullOrEmpty(accountNo))
                {
                    return (new APIResponseHandler<long>
                    {
                        isSuccess = false,
                        Message = "Invalid user ID or account number.",
                        Data = 0
                    });
                }
                var accountbalance=  await _context.TransactionDetails.Where(Td=>Td.AccountNo == accountNo && Td.userID == userID)
                    .OrderByDescending(td => td.TransactionDate)
                    .Select(td => td.ClearBalance)
                    .FirstOrDefaultAsync();
                if ( accountbalance == null)
                    {
                    return (new APIResponseHandler<long>
                    {
                        isSuccess = false,
                        Message = $"No transactions found for account number: {accountNo} and user ID: {userID}",
                        Data = 0
                    });
                }
                return (new APIResponseHandler<long>
                {
                    isSuccess = true,
                    Message = "User account balance fetched successfully.",
                    Data = accountbalance
                });


            }
            catch (Exception ex)
            {
                return (new APIResponseHandler<long>
                {
                    isSuccess = false,
                    Message = "An error occurred while fetching user account balance. Message=" + ex,
                    Data = 0
                });
            }
        }

        public async Task<APIResponseHandler<UserAccountDetail>> GetUserAccountDetailsByAccountNoRepositoryAsync(string accountNo)
        {
            try 
            {
                // Check if the account number is valid
                if (string.IsNullOrEmpty(accountNo))
                {
                    return (new APIResponseHandler<UserAccountDetail>
                    {
                        isSuccess = false,
                        Message = "Account number cannot be null or empty.",
                        Data = null
                    });
                }
                // Fetch the user account details by account number
                var userAccountDetails = await _context.UserAccountDetails
                    .FirstOrDefaultAsync(ua => ua.AccountNo == accountNo);
                if (userAccountDetails == null)
                {
                    return (new APIResponseHandler<UserAccountDetail>
                    {
                        isSuccess = false,
                        Message = $"User account details not found for account number: {accountNo}",
                        Data = null
                    });
                }
                return (new APIResponseHandler<UserAccountDetail>
                {
                    isSuccess = true,
                    Message = "User account details fetched successfully.",
                    Data = userAccountDetails
                });


            }
            catch(Exception ex)
            {
                return (new APIResponseHandler<UserAccountDetail>
                {
                    isSuccess = false,
                    Message = "An error occurred while fetching user account details. Message=" + ex,
                    Data = null,
                    
                });

            }
            
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
