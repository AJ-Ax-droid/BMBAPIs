using BankingWebAPI.BLL.Interface;
using BankingWebAPI.DAL.DtoClass;
using BankingWebAPI.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MYBankingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionDetailController : Controller
    {
        private readonly ITransactionDetailService _transactionDetailService;
        public TransactionDetailController(ITransactionDetailService transactionDetailService)
        {
            _transactionDetailService = transactionDetailService;
        }
        [HttpGet("GetAllTransactionDetailsByAccountNumber/{accountNo}")]
        public async Task<IActionResult> GetAllTransactionDetailsByAccountNumber()
        {
            string accountNo = HttpContext.Request.RouteValues["accountNo"]?.ToString();
            if (string.IsNullOrEmpty(accountNo))
            {
                return BadRequest("Account number is required.");
            }
            try
            {
                var transactionDetails = await _transactionDetailService.GetAllTransactionDetailsByAccountNumberServiceAsync(accountNo);
                if (transactionDetails == null || !transactionDetails.Any())
                {
                    return NotFound($"No transaction details found for account number: {accountNo}");
                }
                return Ok(transactionDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while fetching transaction details: {ex.Message}");
            }


        }

        [HttpPost("MakeCreditTransactioninAccount")]
        public async Task<IActionResult> MakeCreditTransactioninAccount([FromBody] MakeTransaction makeTransaction)
        {
            if (makeTransaction == null)
            {
                return BadRequest("Invalid transaction data.");
            }
            try
            {
                var result = await _transactionDetailService.MakeCreditTransactioninAccountServiceAsync(makeTransaction);
                if (result)
                {
                    return Ok("Transaction completed successfully.");
                }
                else
                {
                    return BadRequest("Failed to complete the transaction.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while processing the transaction: {ex.Message}");
            }


        }
    }
}
