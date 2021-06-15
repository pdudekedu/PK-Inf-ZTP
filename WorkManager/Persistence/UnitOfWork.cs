using System.Threading.Tasks;
using WorkManager.Persistence.Repositories;

namespace WorkManager.Persistence
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
        IUserRepository Users { get; }
        IResourceRepository Resources { get; }
        ITeamRepository Teams { get; }
        IProjectRepository Projects { get; }
        TaskRepository Tasks { get; }
    }
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
            Users = new UserRepository(context);
            Resources = new ResourceRepository(context);
            Teams = new TeamRepository(context);
            Projects = new ProjectRepository(context);
            Tasks = new TaskRepository(context);
        }

        public IUserRepository Users { get; }
        public IResourceRepository Resources { get; }
        public ITeamRepository Teams { get; }
        public IProjectRepository Projects { get; }
        public TaskRepository Tasks { get; }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
