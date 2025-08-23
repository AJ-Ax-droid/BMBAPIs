using BankingWebAPI.BLL.Interface;
using BankingWebAPI.BLL.Repository.HelperMethods;
using BankingWebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingWebAPI.BLL.helper;
using BankingWebAPI.DAL.DtoClass;

namespace BankingWebAPI.BLL.Service
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly AccountsHelperRepo _accountsHelperRepo;
        public UserAccountService(IUserAccountRepository userAccountRepository, AccountsHelperRepo accountsHelperRepo)
        {
            _userAccountRepository = userAccountRepository;
            _accountsHelperRepo = accountsHelperRepo;
        }

        public Task<APIResponseHandler<UserAccountDetail>> CreateUserAccountDetailsServiceAsync(UserAccountDetail userAccountDetail)
        {
            if (userAccountDetail == null)
            {
                throw new ArgumentNullException(nameof(userAccountDetail), "UserAccountDetail cannot be null");
            }
            var GetAccuserdtl = new AccountNo
            {
                FirstName = userAccountDetail.FirstName,
                LastName = userAccountDetail.LastName,
                PanNo = userAccountDetail.PanNo,
                Account_Type = userAccountDetail.Account_Type,
            };
            string accountNumber = AccountDetail.GetAccountNumber(GetAccuserdtl);
            userAccountDetail.AccountNo = accountNumber; 
            return _userAccountRepository.CreateUserAccountDetailsRepositoryAsync(userAccountDetail);

        }

        public Task<APIResponseHandler<long>> GetUserAccountBalanceByAccountNoAndUserIdServiceAsync(int userID, string AccountNo)
        {
            return _userAccountRepository.GetUserAccountBalanceByAccountNoAndUserIdRepositoryAsync(userID, AccountNo);

        }

        public async Task<List<UserAccountDetail>> GetUSerAccountDetailsByUserIDServiceAsync(int userID)
        {
            // Call the repository method to get user account details by user ID
            return await _userAccountRepository.GetUSerAccountDetailsByUserIDRepositoryAsync(userID);
        }

        public async Task<APIResponseHandler<bool>> IsAccountExistinBMB(string AccountNo)
        {
            try
            {
                // Call  the helper method to check if the account exists
                var isAccountExists = await _accountsHelperRepo.IsAccountExistsAsync(AccountNo);
                return new APIResponseHandler<bool>
                {
                    isSuccess = isAccountExists,
                    Message = isAccountExists ? "Account exists in BMB." : "Account does not exist in BMB.",
                    Data = isAccountExists
                };

            }
            catch (Exception ex)
            {
                // Log the exception (if logging is set up)
                // _logger.LogError(ex, "Error checking if account exists in BMB");
                return (new APIResponseHandler<bool>
                {
                    isSuccess = false,
                    Message = "An error occurred while checking account existence.Message= " + ex,
                    Data = false
                });

            }
        }
    }
}
