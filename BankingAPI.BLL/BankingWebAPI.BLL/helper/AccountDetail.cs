using BankingWebAPI.DAL.DtoClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingWebAPI.BLL.helper
{
    public static class AccountDetail
    {
        public static string GetAccountNumber(AccountNo user)
        {
            if (user == null || user == null)
                throw new ArgumentNullException(nameof(user), "User or UserAccountDetais cannot be null");


            var pan = user.PanNo;
            var firstName = user.FirstName;
            var lastName = user.LastName;
            var accountType = user.Account_Type;

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
            string randomint = GenerateRandomIntNumber();
            string firstLetterName = firstName.Substring(0, 1).ToUpper();
            string firstLetterLastName = lastName.Substring(0, 1).ToUpper();
            string startTwoPan = pan.Substring(0, 2).ToUpper();
            string firstDigitAccountType = accountType.Substring(0, 1).ToUpper();
            string accountName = $"{randomint.Substring(5)}{firstLetterName}MP{firstLetterLastName}{startTwoPan}BMB{firstDigitAccountType}";
            return accountName;
        }

        public static string GenerateRandomIntNumber()
        {
            Random random = new Random();
            const string digits = "0123456789";
            return new string(Enumerable.Repeat(digits, 10)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }


    }
}
