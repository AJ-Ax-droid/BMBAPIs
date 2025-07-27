using System.ComponentModel.DataAnnotations;

namespace BankingWebAPI.DAL.Models;

public partial class UserLoginDetails
{
    [Key]
    public int UserID { get; set; }

    [Required]
    public string UserName { get; set; }

    [Required]
    
    public string Password { get; set; } = null!;

    public string? UserEmail { get; set; }
}
