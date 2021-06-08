using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkManager.Persistence.Entities;

namespace WorkManager.Persistence.Repositories
{
    public class UserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public Task<User> GetAsync(int id)
        {
            return _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<User>> GetAllAsync()
        {
            return _context.Users.ToListAsync();
        }

        public Task<User> GetByUserNameAsync(string userName)
        {
            return _context.Users.FirstOrDefaultAsync(x => x.UserName == userName);
        }

        public void AddAsync(User user)
        {
            _context.Users.Add(user);
        }
    }
}
