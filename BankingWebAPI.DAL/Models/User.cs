using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingWebAPI.DAL.Models;

public partial class User
{
    [Key]
    public int UserID { get; set; }

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string Address { get; set; } = null!;

    public string EmailID { get; set; } = null!;

    public long PhoneNumber { get; set; }

    public bool? IsActive { get; set; }

    public DateTime CreateON { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? ModifiedOn { get; set; }

    public string? ModifiedBy { get; set; }

    //public virtual UserAccountDetail UserAccountDetais { get; set; }

    
    //public virtual UserLoginDetails? UserLoginDetails { get; set; }


}
