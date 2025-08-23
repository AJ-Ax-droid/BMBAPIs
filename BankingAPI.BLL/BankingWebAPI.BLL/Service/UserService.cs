using BankingWebAPI.BLL.Interface;
using BankingWebAPI.DAL.DtoClass;
using BankingWebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BankingWebAPI.BLL.helper;

namespace BankingWebAPI.BLL.Service
{
    
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }
        public async Task<GetUserDetails> CreateUserAccountAsync(RegisterNewUsercs user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null");
            }
           var GetAccuserdtl= new AccountNo
            {
                FirstName = user.User.FirstName,
                LastName = user.User.LastName,
                PanNo = user.UserAccountDetais.PanNo,
                Account_Type = user.UserAccountDetais.Account_Type,
            };
            string accountNumber = AccountDetail.GetAccountNumber(GetAccuserdtl);
            user.UserAccountDetais.AccountNo = accountNumber;

            return await _userRepository.CreateUserAccountAsync(user,accountNumber);
        }

        
    }
}
