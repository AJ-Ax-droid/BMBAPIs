using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankingWebAPI.DAL.Models;

public partial class TransactionDetail
{
    public string AccountNo { get; set; } = null!;

    public int userID { get; set; }

    [Key]
    public string TransactionID { get; set; } = null!;

    public string TransactionType { get; set; } = null!;
    public decimal PreviousAmmount { get; set; }// Amount before Transactions

    public decimal AmountTrasacted { get; set; } // Amount to be Transacted

    public long ClearBalance { get; set; } // Balance after Transactions

    public string TransactionStatus { get; set; } = null!;

    public DateTime TransactionDate { get; set; }

}
