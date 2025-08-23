using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BankingWebAPI.DAL.Models;
using BankingWebAPI.BLL.Interface;

namespace MYBankingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountDetailController : ControllerBase
    {
        private readonly IUserAccountService _userAccountService;
        public UserAccountDetailController(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }
        // Get user account details by user ID
        [HttpGet("GetUserAccountDetailsByUserID/{userID}")]
        public async Task<IActionResult> GetUserAccountDetailsByUserID()
        {
            int userID;
            if (!int.TryParse(HttpContext.Request.RouteValues["userID"]?.ToString(), out userID) || userID <= 0)
            {
                return BadRequest("Invalid user ID.");
            }
            try
            {
                var userAccountDetails = await _userAccountService.GetUSerAccountDetailsByUserIDServiceAsync(userID);
                if (userAccountDetails == null)
                {
                    return NotFound($"User account details not found for user ID: {userID}");
                }
                return Ok(userAccountDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while fetching user account details: {ex.Message}");
            }
        }

        [HttpGet("GetUserAccountBalanceByAccountNoAndUserId")]
        public async Task<IActionResult> GetUserAccountBalanceByAccountNoAndUserIdAsync(int userID, string accountNo) {
            try
            {
                if (userID <= 0 || string.IsNullOrEmpty(accountNo))
                {
                    return BadRequest("Invalid user ID or account number.");
                }
                var accountBalance = await _userAccountService.GetUserAccountBalanceByAccountNoAndUserIdServiceAsync(userID, accountNo);
                return Ok(new
                {
                    isSuccess = accountBalance.isSuccess,
                    Message = accountBalance.Message,
                    Data = accountBalance.Data
                });


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while fetching user account balance: {ex.Message}");

            }
        
        
        }
        [HttpGet("IsAccountExistinBMB")]
        public async Task<IActionResult> IsAccountExistinBMBAsync(string accountNo)
        {
            try
            {
                if (string.IsNullOrEmpty(accountNo))
                {
                    return BadRequest("Invalid account number.");
                }
                var isAccountExists = await _userAccountService.IsAccountExistinBMB(accountNo);
                return Ok(new
                {
                    isSuccess = isAccountExists.isSuccess,
                    Message = isAccountExists.Message,
                    Data = isAccountExists.Data
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while checking account existence: {ex.Message}");
            }
        }

        [HttpPost("CreateUserAccountDetails")]
        public async Task<IActionResult> CreateUserAccountDetailsAsync(UserAccountDetail userAccountDetail)
        {
            if (userAccountDetail == null)
            {
                return BadRequest("UserAccountDetail cannot be null");
            }
            try
            {
                var createdAccountDetail = await _userAccountService.CreateUserAccountDetailsServiceAsync(userAccountDetail);
                if (createdAccountDetail == null || !createdAccountDetail.isSuccess)
                {
                    return BadRequest("Failed to create user account details");
                }
                return Ok(createdAccountDetail);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while creating user account details: {ex.Message}");
            }
        }

    }
}
