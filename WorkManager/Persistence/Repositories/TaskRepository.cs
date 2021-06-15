using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkManager.Persistence.Repositories
{
    public interface ITaskRepository : IRepository<Entities.Task>
    {
        Task<List<Entities.Task>> GetByProjectId(int projectId);
    }

    public class TaskRepository : Repository<Persistence.Entities.Task>, ITaskRepository
    {
        public TaskRepository(DataContext context) : base(context)
        {

        }
        public async Task<List<Entities.Task>> GetByProjectId(int projectId)
        {
            return await Context.Tasks.Where(x => x.ProjectId == projectId).ToListAsync();
        }
    }
}
