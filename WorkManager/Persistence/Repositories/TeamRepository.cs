using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkManager.Persistence.Entities;

namespace WorkManager.Persistence.Repositories
{
    public class TeamRepository : Repository<Team>
    {
        public TeamRepository(DataContext context) : base(context)
        {

        }
        public async override Task<List<Team>> GetAllAsync()
        {
            return await Context.Teams
                .Include(x => x.Users)
                .Where(x => x.InUse)
                .ToListAsync();
        }
    }
}
