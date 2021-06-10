using System.Threading.Tasks;
using WorkManager.Persistence.Repositories;

namespace WorkManager.Persistence
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
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

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
