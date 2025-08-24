using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingWebAPI.DAL.Models
{
    public class TransferAmountBtoB
    {
        public bool isReceiverAccountVerifiedBMB { get; set; }
        public bool isQRCodeScanned { get; set; }
        public string ReceiverAccountNo { get; set; }
        public string ReceiverAccountHolderName { get; set; }
        public int SenderUserID { get; set; }
        public string SenderAccountNo { get; set; }
        public string SenderAccoundHolderName { get; set; }
        public long AmountToSend { get; set; }
    }
}
