using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingWebAPI.DAL.Models;

public partial class UserAccountDetail
{
    
    public int UserID { get; set; }

    public string PanNo { get; set; } = null!;
    [Key]
    public string? AccountNo { get; set; } = null!;

    public string Account_Type { get; set; } = null!;
    public DateTime AccountCreatedOn { get; set; }
    [NotMapped]
    public string FirstName { get; set; } = null!;
    [NotMapped]
    public string LastName { get; set; } = null!;


}
