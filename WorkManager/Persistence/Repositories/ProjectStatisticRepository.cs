using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkManager.Persistence.Entities;

namespace WorkManager.Persistence.Repositories
{
    public interface IProjectStatisticRepository 
    {
        Task<List<ProjectStatistic>> GetAllAsync();
    }
    public class ProjectStatisticRepository : IProjectStatisticRepository
    {
        private readonly DataContext context;

        public ProjectStatisticRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<List<ProjectStatistic>> GetAllAsync()
        {
            return await context.ProjectsStatistics.ToListAsync();
        }
    }

}
