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
        public string receiverAccountNo { get; set; }
        public int ReceiverUserID { get; set; }
        public long AmountToTransfer { get; set; }
        public string TransactionType { get; set; } 
        public string TransactionBy { get; set; }
        public long senderAccountBalance { get; set; }
        public long receiverAccountBalance { get; set; }

    }
}
