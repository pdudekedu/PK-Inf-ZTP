using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkManager.Persistence.Entities;

namespace WorkManager.Persistence.Repositories
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<List<Project>> GetAllFor(int userId);
    }
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(DataContext context) : base(context)
        {

        }
        public async override Task<Project> GetAsync(int id)
        {
            return await InUse
                .Include(x => x.Resources)
                .Include(x => x.Team)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async override Task<List<Project>> GetAllAsync()
        {
            return await InUse
                .Include(x => x.Resources)
                .Include(x => x.Team)
                .ToListAsync();
        }

        public async Task<List<Project>> GetAllFor(int userId)
        {
            return await InUse
                .Where(p => p.Team.Users.Any(u => u.Id == userId))
                .ToListAsync();
        }
    }
}
