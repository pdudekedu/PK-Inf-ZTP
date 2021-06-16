using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkManager.Persistence.Entities;

namespace WorkManager.Persistence.Repositories
{
    public interface IUserStatisticRepository 
    {
        Task<List<UserStatistic>> GetAllAsync();
    }
    public class UserStatisticRepository : IUserStatisticRepository
    {
        private readonly DataContext context;

        public UserStatisticRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<List<UserStatistic>> GetAllAsync()
        {
            return await context.UsersStatistics.ToListAsync();
        }
    }

}
