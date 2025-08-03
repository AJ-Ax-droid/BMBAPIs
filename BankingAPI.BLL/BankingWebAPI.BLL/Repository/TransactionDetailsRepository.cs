using BankingWebAPI.BLL.Interface;
using BankingWebAPI.DAL;
using BankingWebAPI.DAL.DtoClass;
using BankingWebAPI.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingWebAPI.BLL.Repository
{
    public class TransactionDetailsRepository : ITransactionDetailRepository
    {
        private readonly BankingDbContext _context;
        public TransactionDetailsRepository(BankingDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TransactionDetail>> GetAllTransactionDetailsByAccountNumberRepositoryAsync(string accountNo)
        {
            try
            {
                var transactionDetails = await _context.TransactionDetails
                    .Where(td => td.AccountNo == accountNo)
                    .ToListAsync();
                return transactionDetails;
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new Exception("An error occurred while retrieving transaction details.", ex);
            }

        }

        public async Task<Boolean> MakeCreditTransactioninAccountRepositoryAsync(MakeTransaction makeTransaction)
        {
            try
            {
                var isUserExists = await _context.Users.AnyAsync(u => u.UserID == makeTransaction.UserID);
                if (!isUserExists)
                {
                    throw new Exception("User does not exist.");

                }
                var isAccountExitstforthatUser = await _context.UserAccountDetails.AnyAsync(a => a.AccountNo == makeTransaction.AccountNO && a.UserID == makeTransaction.UserID);
                if (!isAccountExitstforthatUser)
                {
                    throw new Exception("Account does not exist for the user.");

                }


                var sql = "CALL public.make_credit_transaction({0}, {1}, {2}, {3}, {4})";
                var rowsAffected = await _context.Database.ExecuteSqlRawAsync(
                    sql,
                    makeTransaction.Amount,
                    makeTransaction.UserID, // Assuming this is userId, adjust if needed
                    makeTransaction.TransactionType,
                    makeTransaction.AccountNO,
                    makeTransaction.TransactionBy
                );
                return true;
                
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new Exception("An error occurred while making a debit transaction.", ex);
            }



        }
    }
}
