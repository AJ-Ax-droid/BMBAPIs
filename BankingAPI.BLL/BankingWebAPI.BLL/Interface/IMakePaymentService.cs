using BankingWebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingWebAPI.BLL.Interface
{
    public interface IMakePaymentService
    {
        public Task<APIResponseHandler<bool>> MakePaymentToAnotherAccountServiceAsync(bool isReceiverAccountVerifiedBMB,bool isQRScanned, string ReceiverAccountNo, string ReceiverAccountHolderName,int SenderUserID, string SenderAccountNo, string SenderAccoundHolderName,long AmountToSend);
        
    }
}
