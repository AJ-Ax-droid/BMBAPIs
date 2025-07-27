using BankingWebAPI.DAL.DtoClass;
using BankingWebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingWebAPI.BLL.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<GetUserDetails> CreateUserAccountAsync(RegisterNewUsercs user);


    }
}
