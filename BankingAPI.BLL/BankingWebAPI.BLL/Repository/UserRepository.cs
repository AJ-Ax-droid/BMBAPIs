using BankingWebAPI.BLL.Interface;
using BankingWebAPI.DAL;
using BankingWebAPI.DAL.DtoClass;
using BankingWebAPI.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingWebAPI.BLL.Repository
{

    public class UserRepository : IUserRepository
    {
        private readonly BankingDbContext _context;
        public UserRepository(BankingDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<GetUserDetails> CreateUserAccountAsync(RegisterNewUsercs user,string AccountNo)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            try
            {
                // Save User first
                _context.Users.Add(user.User);
                await _context.SaveChangesAsync();
                // After saving user, we can access the UserID
                var userId = user.User.UserID;

                user.UserAccountDetais.AccountNo = $"{AccountNo}{userId}";
                user.UserAccountDetais.UserID = userId;
                user.UserLoginDetails.UserID = userId;
                _context.UserAccountDetails.Add(user.UserAccountDetais);
                _context.UserLoginDetail.Add(user.UserLoginDetails);
                await _context.SaveChangesAsync();

                return new GetUserDetails
                {
                    UserID = user.User.UserID,
                    UserName = user.UserLoginDetails.UserName,
                    FirstName = user.User.FirstName,
                    LastName = user.User.LastName,
                    Address = user.User.Address,
                    EmailID = user.User.EmailID,
                    PhoneNumber = user.User.PhoneNumber,
                    AccountNo = user.UserAccountDetais.AccountNo,
                    Account_Type = user.UserAccountDetais.Account_Type,
                    PanNo = user.UserAccountDetais.PanNo
                };
            }
            catch (Exception ex)
            {
                throw new Exception("some error occured" + ex);
            }
        }

        public Task<APIResponseHandler<User>> GetUserDetailsByUserIDAsync(int userId)
        {
            try
            {
                var user = _context.Users.FirstOrDefaultAsync(u => u.UserID == userId);
                if (user == null)
                {
                    return Task.FromResult(new APIResponseHandler<User>
                    {
                        isSuccess = false,
                        Message = "User not found.",
                        Data = null
                    });
                }
                return Task.FromResult(new APIResponseHandler<User>
                {
                    isSuccess = true,
                    Message = "User details fetched successfully.",
                    Data = user.Result
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new APIResponseHandler<User>
                {
                    isSuccess = false,
                    Message = "An error occurred while fetching user details. Message=" + ex.Message,
                    Data = null
                });
            }

        }
    }
}
