using Microsoft.AspNetCore.Mvc;
using BankingWebAPI.DAL.Models;
using Microsoft.EntityFrameworkCore;
using BankingWebAPI.BLL.Interface;
using BankingWebAPI.DAL.DtoClass;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MYBankingAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService )
        {
            _userService = userService;
        }
        // GET: api/<UserController>
        [HttpGet]
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userService.GetAllUsersAsync();
        }
        //Creaet a new user
        [HttpPost]
        public
            async Task<ActionResult<GetUserDetails>> CreateUser(RegisterNewUsercs user)
        {
            if (user == null)
            {
                return BadRequest("User cannot be null");
            }
            try
            {
                var userAccountDetail = await _userService.CreateUserAccountAsync(user);
                if (userAccountDetail == null)
                {
                    return BadRequest("Failed to create user account");
                }
                return Ok(userAccountDetail);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

    }
}
