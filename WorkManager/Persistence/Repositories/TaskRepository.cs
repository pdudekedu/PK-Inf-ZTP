using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkManager.Persistence.Repositories
{
    public interface ITaskRepository : IRepository<Entities.Task>
    {
        Task<List<Entities.Task>> GetByProjectId(int projectId);
        Task<Entities.Task> GetByProjectId(int projectId, int id);
    }

    public class TaskRepository : Repository<Persistence.Entities.Task>, ITaskRepository
    {
        public TaskRepository(DataContext context) : base(context)
        {

        }
        public async Task<List<Entities.Task>> GetByProjectId(int projectId)
        {
            return await Context.Tasks
                .Include(x => x.Resources)
                .Include(x => x.User)
                .Where(x => x.ProjectId == projectId).ToListAsync();
        }
        public async Task<Entities.Task> GetByProjectId(int projectId, int id)
        {
            return await Context.Tasks
                .Include(x=>x.Resources)
                .Include(x=>x.User)
                .FirstOrDefaultAsync(x => x.ProjectId == projectId && x.Id == id);
        }
    }
}
