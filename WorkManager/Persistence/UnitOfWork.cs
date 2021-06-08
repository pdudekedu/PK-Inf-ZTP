using System.Threading.Tasks;
using WorkManager.Persistence.Repositories;

namespace WorkManager.Persistence
{
    public interface IUnitOfWork
    {
        Task Commit();
        UserRepository Users { get; }
    }
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
            Users = new UserRepository(context);
        }

        public UserRepository Users { get; }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}
