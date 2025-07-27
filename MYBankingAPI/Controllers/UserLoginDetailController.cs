using BankingWebAPI.BLL.Interface;
using BankingWebAPI.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MYBankingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginDetailController : ControllerBase
    {
        private readonly IUserLoginService _userLoginService;
        public UserLoginDetailController(IUserLoginService userLoginService)
        {
            _userLoginService = userLoginService;
        }
        
        // Varify user login
        [HttpGet("VerifyLogin")]
        public async Task<ActionResult<UserLoginDetails>> VerifyLogin(string username, string password)
        {
            try
            {
                var user = await _userLoginService.LoginAsync(username, password);
                if (user == null)
                {
                    return Unauthorized("Invalid username or password.");
                }
                return Ok(user);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request: " + ex.Message);
            }
        }
    }
}
