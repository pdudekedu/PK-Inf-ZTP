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
        public async Task<bool> ExistsUsersAsync(IReadOnlyList<int> ids)
        {
            return await (InUse.Where(x => ids.Contains(x.Id)).CountAsync()) == ids.Count;
        }
        
    }
}
