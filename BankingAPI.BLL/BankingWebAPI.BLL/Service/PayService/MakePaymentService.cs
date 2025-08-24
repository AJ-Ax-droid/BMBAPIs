using BankingWebAPI.BLL.Interface;
using BankingWebAPI.BLL.Repository.HelperMethods;
using BankingWebAPI.DAL.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingWebAPI.BLL.Service.PayService
{
    public class MakePaymentService : IMakePaymentService
    {
        private readonly ILogger<MakePaymentService> _logger;
        private readonly AccountsHelperRepo _accountsHelperRepo;
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITransactionDetailRepository _transactionDetailRepository;
        public MakePaymentService(ILogger<MakePaymentService> logger, AccountsHelperRepo accountsHelperRepo, IUserAccountRepository userAccountRepository, IUserRepository userRepository,ITransactionDetailRepository transactionDetailRepository )
        {
            _accountsHelperRepo = accountsHelperRepo;
            _transactionDetailRepository = transactionDetailRepository;
            _logger = logger;
            _userAccountRepository = userAccountRepository;
            _userRepository = userRepository;
        }
        public async Task<APIResponseHandler<bool>> MakePaymentToAnotherAccountServiceAsync(bool isReceiverAccountVerifiedBMB, string ReceiverAccountNo, string ReceiverAccountHolderName, int SenderUserID, string SenderAccountNo, string SenderAccoundHolderName, long AmountToSend)
        {
            if (isReceiverAccountVerifiedBMB)
            {
                //Check if ReceverAccount is exist in the database or not and get the name of that accountholder if it matches or not
                var isRaccExist = await _accountsHelperRepo.IsAccountExistsAsync(ReceiverAccountNo);
                if (!isRaccExist)
                {
                    return (new APIResponseHandler<bool>
                    {
                        isSuccess = false,
                        Message = "Receiver account does not exist.",
                        Data = false
                    });
                }
                var ReceiverAccountdtl = await _userAccountRepository.GetUserAccountDetailsByAccountNoRepositoryAsync(ReceiverAccountNo);
                if (ReceiverAccountdtl == null || ReceiverAccountdtl.Data == null)
                {
                    return (new APIResponseHandler<bool>
                    {
                        isSuccess = false,
                        Message = "Receiver account details not found.",
                        Data = false
                    });
                }
                var ReceiverUserDetails = await _userRepository.GetUserDetailsByUserIDAsync(ReceiverAccountdtl.Data.UserID);
                if (ReceiverUserDetails == null || ReceiverUserDetails.Data == null)
                {
                    return (new APIResponseHandler<bool>
                    {
                        isSuccess = false,
                        Message = "Receiver user details not found.",
                        Data = false
                    });
                }
                else if (!(ReceiverAccountHolderName).Contains(ReceiverUserDetails.Data.FirstName))
                {
                    return (new APIResponseHandler<bool>
                    {
                        isSuccess = false,
                        Message = "Receiver account holder name does not match.",
                        Data = false
                    });
                }


                //check if the sender account is valid and has sufficient balance
                var SenderAccountDetails = await _userAccountRepository.GetUserAccountDetailsByAccountNoRepositoryAsync(SenderAccountNo);
                if (SenderAccountDetails == null || SenderAccountDetails.Data == null)
                {
                    return (new APIResponseHandler<bool>
                    {
                        isSuccess = false,
                        Message = "Sender account details not found.",
                        Data = false
                    });
                }
                var SenderUserDetails = await _userRepository.GetUserDetailsByUserIDAsync(SenderUserID);
                if (SenderUserDetails == null || SenderUserDetails.Data == null)
                {
                    return (new APIResponseHandler<bool>
                    {
                        isSuccess = false,
                        Message = "Sender user details not found.",
                        Data = false
                    });
                }
                var senderBlnc = _userAccountRepository.GetUserAccountBalanceByAccountNoAndUserIdRepositoryAsync(SenderUserID, SenderAccountNo).Result.Data;
                if (AmountToSend > senderBlnc)
                {
                    return (new APIResponseHandler<bool>
                    {
                        isSuccess = false,
                        Message = "Sender account does not have sufficient balance.",
                        Data = false
                    });
                }
                // Transfer the amount from sender to receiver account
                var transferResult = await _transactionDetailRepository.InsertTransactiondata(new TransferBalance
                {
                    SenderAccountNo = SenderAccountNo,
                    SenderUserID = SenderUserID,
                    receiverAccountNo = ReceiverAccountNo,
                    ReceiverUserID = ReceiverAccountdtl.Data.UserID,
                    AmountToTransfer = AmountToSend,
                    TransactionType = "Payment",
                    TransactionBy = SenderUserDetails.Data.FirstName + " " + SenderUserDetails.Data.LastName,
                    receiverAccountBalance=  _userAccountRepository.GetUserAccountBalanceByAccountNoAndUserIdRepositoryAsync(ReceiverAccountdtl.Data.UserID, ReceiverAccountNo).Result.Data,
                    senderAccountBalance = senderBlnc
                });
                if (transferResult == null || !transferResult.isSuccess)
                {
                    return (new APIResponseHandler<bool>
                    {
                        isSuccess = false,
                        Message = "Failed to process the payment.",
                        Data = false
                    });
                }
                return (new APIResponseHandler<bool>
                {
                    isSuccess = transferResult.isSuccess,
                    Message = transferResult.Message,
                    Data = transferResult.Data
                });
            }
            else
            {
                // If the receiver account is not verified, we can throw an exception or return false
                return (new APIResponseHandler<bool>
                {
                    isSuccess = false,
                    Message = "Receiver account is not verified.",
                    Data = false
                });
            }

        }
     
    }
}
