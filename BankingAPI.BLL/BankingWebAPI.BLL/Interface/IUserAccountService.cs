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
        
        Task<UserAccountDetail> GetUSerAccountDetailsByUserIDServiceAsync(int userID);
        
    }
}
