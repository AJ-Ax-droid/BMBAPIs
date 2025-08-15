using BankingWebAPI.DAL.DtoClass;
using BankingWebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingWebAPI.BLL.Interface
{
    public interface ITransactionDetailRepository
    {
        Task<IEnumerable<TransactionDetail>> GetAllTransactionDetailsByAccountNumberRepositoryAsync(string accountNo);
        Task<Boolean> MakeCreditTransactioninAccountRepositoryAsync(MakeTransaction makeTransaction);
        Task<APIResponseHandler<Boolean>> InsertTransactiondata(TransferBalance transactionDetail);

    }
}
