using BankingWebAPI.BLL.Interface;
using BankingWebAPI.DAL.DtoClass;
using BankingWebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingWebAPI.BLL.Service
{
    public class TransactionDetailsService : ITransactionDetailService
    {
        private readonly ITransactionDetailRepository _transactionDetailRepository;
        public TransactionDetailsService(ITransactionDetailRepository transactionDetailRepository)
        {
            _transactionDetailRepository = transactionDetailRepository;
        }

        public Task<bool> MakeCreditTransactioninAccountServiceAsync(MakeTransaction makeTransaction)
        {
                return _transactionDetailRepository.MakeCreditTransactioninAccountRepositoryAsync(makeTransaction);
        }

        async Task<IEnumerable<TransactionDetail>> ITransactionDetailService.GetAllTransactionDetailsByAccountNumberServiceAsync(string accountNo)
        {
            try
            {
                return await _transactionDetailRepository.GetAllTransactionDetailsByAccountNumberRepositoryAsync(accountNo);
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new Exception("An error occurred while retrieving transaction details.", ex);
            }
        }
    }
}
