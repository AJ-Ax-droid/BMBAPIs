using BankingWebAPI.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingWebAPI.BLL.Repository.HelperMethods
{
    public class AccountsHelperRepo
    {
        private readonly BankingDbContext _context;
        public AccountsHelperRepo(BankingDbContext context)
        {
            _context = context;
        }
        public async Task<bool> IsAccountExistsAsync(string accountNo)
        {
            return await _context.UserAccountDetails.AnyAsync(a => a.AccountNo == accountNo);
        }
    }
}
