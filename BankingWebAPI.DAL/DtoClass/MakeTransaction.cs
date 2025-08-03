using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingWebAPI.DAL.DtoClass
{
    public class MakeTransaction
    {
        public string AccountNO { get; set; }
        public int UserID { get; set; } // Account holder's user ID, can be set to current user by default
        public decimal Amount { get; set; }
        public string TransactionType { get; set; } // "Debit" or "Credit"
        public string Description { get; set; } // Optional description for the transaction
        public DateTime TransactionDate { get; set; } // Date of the transaction, can be set to current date by default
        public string TransactionBy { get; set; } // User who is making the transaction, can be set to current user by default
        public string TransactionStatus { get; set; } // Status of the transaction, e.g., "Pending", "Completed", "Failed"
        

    }
}
