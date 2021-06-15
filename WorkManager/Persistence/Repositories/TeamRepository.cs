using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkManager.Persistence.Entities;

namespace WorkManager.Persistence.Repositories
{
    public interface ITeamRepository : IRepository<Team>
    {
        
    }
    public class TeamRepository : Repository<Team>, ITeamRepository
    {
        public TeamRepository(DataContext context) : base(context)
        {

        }
        public async override Task<Team> GetAsync(int id)
        {
            return await InUse
                .Include(x => x.Users)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async override Task<List<Team>> GetAllAsync()
        {
            return await InUse
                .Include(x => x.Users)
                .ToListAsync();
        }
    }
}
