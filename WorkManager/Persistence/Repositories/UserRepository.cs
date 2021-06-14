using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkManager.Persistence.Entities;

namespace WorkManager.Persistence.Repositories
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(DataContext context) : base(context)
        {
            
        }

        public async Task<User> GetByUserNameAsync(string userName)
        {
            return await InUse.FirstOrDefaultAsync(x => x.UserName == userName);
        }

        public async Task<bool> ExistsWithUserNameAsync(string userName)
        {
            return await InUse.AnyAsync(x => x.UserName == userName);
        }
        public async Task<bool> ExistsUsersAsync(IReadOnlyList<int> userIds)
        {
            return await (InUse.Where(x => userIds.Contains(x.Id)).CountAsync()) == userIds.Count;
        }
        public async Task<List<User>> GetUsersById(IEnumerable<int> userIds)
        {
            return await InUse.Where(x => userIds.Contains(x.Id)).ToListAsync();
        }
    }
}
