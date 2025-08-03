using BankingWebAPI.DAL.DtoClass;
using BankingWebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingWebAPI.BLL.Interface
{
    public interface ITransactionDetailService
    {
        Task<IEnumerable<TransactionDetail>> GetAllTransactionDetailsByAccountNumberServiceAsync(string accountNo);
        Task<bool> MakeCreditTransactioninAccountServiceAsync(MakeTransaction makeTransaction);

    }
}
