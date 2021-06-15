using System.Threading.Tasks;
using WorkManager.Persistence.Repositories;

namespace WorkManager.Persistence
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
        UserRepository Users { get; }
        ResourceRepository Resources { get; }
        TeamRepository Teams { get; }
        ProjectRepository Projects { get; }
        TaskRepository Tasks { get; }
    }
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
            Users = new(context);
            Resources = new(context);
            Teams = new(context);
            Tasks = new(context);
            Projects = new(context);
        }

        public UserRepository Users { get; }
        public ResourceRepository Resources { get; }
        public TeamRepository Teams { get; }
        public ProjectRepository Projects { get; }
        public TaskRepository Tasks { get; }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
