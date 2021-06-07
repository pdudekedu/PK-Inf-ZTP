using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkManager.Data.Repositories;

namespace WorkManager.Data
{
    public interface IUnitOfWork
    {
        Task Commit();
        AccountRepository Accounts { get; }
    }
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
            Accounts = new AccountRepository(context);
        }

        public AccountRepository Accounts { get; }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}
