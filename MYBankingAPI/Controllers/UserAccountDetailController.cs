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
    }}
