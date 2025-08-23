using BankingWebAPI.DAL.DtoClass;
using BankingWebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingWebAPI.BLL.Interface
{
    public interface IUserAccountService
    {

        Task<List<UserAccountDetail>> GetUSerAccountDetailsByUserIDServiceAsync(int userID);
        Task<APIResponseHandler<decimal>> GetUserAccountBalanceByAccountNoAndUserIdServiceAsync(int userID, string AccountNo);
        Task<APIResponseHandler<bool>> IsAccountExistinBMB(string AccountNo);
        Task<APIResponseHandler<UserAccountDetail>> CreateUserAccountDetailsServiceAsync(UserAccountDetail userAccountDetail);


    }
}
