using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkManager.Persistence.Entities;

namespace WorkManager.Persistence.Repositories
{
    public interface IProjectRepository : IRepository<Project>
    {

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
    }
}
