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
    
        public async Task<APIResponseHandler<Boolean>> InsertTransactiondata (TransferBalance transactionDetail)
        {
            try
            {
                if (transactionDetail == null)
                {
                    return new APIResponseHandler<Boolean>
                    {
                        isSuccess = false,
                        Message = "Transaction detail cannot be null.",
                        Data = false
                    };

                }
                if (string.IsNullOrEmpty(transactionDetail.SenderAccountNo) || string.IsNullOrEmpty(transactionDetail.receiverAccountNo))
                {
                    return new APIResponseHandler<Boolean>
                    {
                        isSuccess = false,
                        Message = "Invalid account number or user ID.",
                        Data = false
                    };
                }
                if (transactionDetail.AmountToTransfer <= 0)
                {
                    return new APIResponseHandler<Boolean>
                    {
                        isSuccess = false,
                        Message = "Transaction amount must be greater than zero.",
                        Data = false
                    };
                }
                if(string.IsNullOrEmpty(transactionDetail.TransactionType))
                {
                    return new APIResponseHandler<Boolean>
                    {
                        isSuccess = false,
                        Message = "Transaction type cannot be null or empty.",
                        Data = false
                    };
                }
                // Add the transaction detail to the context
                //First Make Debit Transaction from Sender Account and check if it is successful

                var debitTransaction = new TransactionDetail
                {
                    AccountNo = transactionDetail.SenderAccountNo,
                    userID = transactionDetail.SenderUserID,
                    AmountTrasacted = transactionDetail.AmountToTransfer,
                    TransactionType = "Debit",
                    TransactionBy = transactionDetail.TransactionBy,
                    TransactionDate = DateTime.UtcNow,
                    ClearBalance = transactionDetail.senderAccountBalance - transactionDetail.AmountToTransfer,
                   PreviousAmmount=transactionDetail.senderAccountBalance,
                    TransactionStatus = "Success",
                    TransactionTo=transactionDetail.ReceiverAccountHolderName,
                };
                var result = await _context.TransactionDetails.AddAsync(debitTransaction);
                if (result == null)
                {
                    return new APIResponseHandler<Boolean>
                    {
                        isSuccess = false,
                        Message = "Failed to make debit transaction.",
                        Data = false
                    };
                }
                if (transactionDetail.ReceiverUserID!=0)
                {     
                

                // Now Make Credit Transaction to Receiver Account
                var creditTransaction = new TransactionDetail
                {
                    AccountNo = transactionDetail.receiverAccountNo,
                    userID = transactionDetail.ReceiverUserID,
                    AmountTrasacted = transactionDetail.AmountToTransfer,
                    TransactionType = "Credit",
                    TransactionBy = transactionDetail.TransactionBy,
                    TransactionDate = DateTime.UtcNow,
                    ClearBalance = transactionDetail.receiverAccountBalance + transactionDetail.AmountToTransfer,
                    PreviousAmmount=transactionDetail.receiverAccountBalance,
                    TransactionStatus = "Success",
                    TransactionTo = transactionDetail.ReceiverAccountHolderName,
                };
                var creditResult = await _context.TransactionDetails.AddAsync(creditTransaction);
                if (creditResult == null) {
                    return new APIResponseHandler<Boolean>
                    {
                        isSuccess = false,
                        Message = "Failed to make credit transaction.",
                        Data = false
                    };
                }
                }
                // Save changes to the database
                await _context.SaveChangesAsync();
                return new APIResponseHandler<Boolean>
                {
                    isSuccess = true,
                    Message = "Amount Transfer Succefully to Account NO."+ transactionDetail.receiverAccountNo,
                    Data = true
                };

            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new Exception("An error occurred while inserting transaction data.", ex);
            }
        }


    }
}
