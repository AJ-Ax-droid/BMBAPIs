using BankingWebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingWebAPI.BLL.Interface
{
    public interface IUserLoginRepository
    {
        Task<User> LoginAsync(string username, string password);
        Task<bool> ChangePasswordAsync(string username, string oldPassword, string newPassword);
        Task<bool> ResetPasswordAsync(string username, string newPassword);
    }
}
