using BankingWebAPI.BLL.Interface;
using BankingWebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingWebAPI.BLL.Service
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IUserAccountRepository _userAccountRepository;
        public UserAccountService(IUserAccountRepository userAccountRepository)
        {
            _userAccountRepository = userAccountRepository;
        }
        public async Task<UserAccountDetail> GetUSerAccountDetailsByUserIDServiceAsync(int userID)
        {
            // Call the repository method to get user account details by user ID
            return await   _userAccountRepository.GetUSerAccountDetailsByUserIDRepositoryAsync(userID);
        }
    }
}
