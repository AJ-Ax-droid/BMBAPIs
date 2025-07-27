using BankingWebAPI.BLL.Interface;
using BankingWebAPI.DAL.DtoClass;
using BankingWebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

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
            string accountNumber = GetAccountNumber(user);
            user.UserAccountDetais.AccountNo = accountNumber;

            return await _userRepository.CreateUserAccountAsync(user,accountNumber);
        }

        public string GetAccountNumber(RegisterNewUsercs user)
        {
            if (user == null || user.UserAccountDetais == null)
                throw new ArgumentNullException(nameof(user), "User or UserAccountDetais cannot be null");

            var pan = user.UserAccountDetais.PanNo;
            var firstName = user.User.FirstName;
            var lastName = user.User.LastName;
            var accountType = user.UserAccountDetais.Account_Type;

            // Defensive checks
            if (string.IsNullOrEmpty(pan) || pan.Length < 2)
                throw new ArgumentException("PanNo must be at least 2 characters long.");
            if (string.IsNullOrEmpty(firstName))
                throw new ArgumentException("FirstName cannot be null or empty.");
            if (string.IsNullOrEmpty(lastName))
                throw new ArgumentException("LastName cannot be null or empty.");
            if (string.IsNullOrEmpty(accountType))
                throw new ArgumentException("Account_Type cannot be null or empty.");

            string endTwoPan = pan.Length >= 2 ? pan.Substring(pan.Length - 2) : pan;
            string firstLetterName = firstName.Substring(0, 1).ToUpper();
            string firstLetterLastName = lastName.Substring(0, 1).ToUpper();
            string startTwoPan = pan.Substring(0, 2).ToUpper();
            string firstDigitAccountType = accountType.Substring(0, 1).ToUpper();

            string accountName = $"{endTwoPan}{firstLetterName}MP{firstLetterLastName}{startTwoPan}BMB{firstDigitAccountType}";
            return accountName;
        }


    }
}
