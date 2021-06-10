using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
            return await Context.Users.FirstOrDefaultAsync(x => x.UserName == userName);
        }
    }
}
