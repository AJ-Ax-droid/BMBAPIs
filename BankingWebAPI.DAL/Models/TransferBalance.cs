using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingWebAPI.DAL.Models
{
    public class TransferBalance
    {
        public string SenderAccountNo { get; set; }
        public int SenderUserID { get; set; }
        public decimal senderAccountBalance { get; set; }
        public string receiverAccountNo { get; set; }
        public int ReceiverUserID { get; set; }
        public string ReceiverAccountHolderName { get; set; }
        public decimal receiverAccountBalance { get; set; }
        public decimal AmountToTransfer { get; set; }
        public string TransactionType { get; set; } 
        public string TransactionBy { get; set; }

    }
}
