using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkManager.Data.Entities;

namespace WorkManager.Data.Repositories
{
    public class AccountRepository
    {
        private readonly DataContext _context;

        public AccountRepository(DataContext context)
        {
            _context = context;
        }

        public Task<Account> GetByUserNameAsync(string userName)
        {
            return _context.Accounts.FirstOrDefaultAsync(x => x.UserName == userName);
        }

        public void AddAsync(Account account)
        {
            _context.Accounts.Add(account);
        }
    }
}
