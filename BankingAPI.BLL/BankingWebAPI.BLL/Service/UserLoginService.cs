using BankingWebAPI.BLL.Interface;
using BankingWebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingWebAPI.BLL.Service
{
    public class UserLoginService :IUserLoginService
    {
        private readonly IUserLoginRepository _userloginRepository;
        public UserLoginService(IUserLoginRepository userloginRepository)
        {
            _userloginRepository = userloginRepository;
        }

        public Task<bool> ChangePasswordAsync(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public async Task<User> LoginAsync(string username, string password)
        {
            return await _userloginRepository.LoginAsync(username, password);

        }

        public Task<bool> ResetPasswordAsync(string username, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}
