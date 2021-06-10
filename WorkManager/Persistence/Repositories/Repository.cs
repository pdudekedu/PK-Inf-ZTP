using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkManager.Persistence.Entities;

namespace WorkManager.Persistence.Repositories
{
    public class Repository<T> where T: Entity
    {
        protected readonly DataContext Context;

        public Repository(DataContext context)
        {
            Context = context;
        }

        public virtual async Task<T> GetAsync(int id)
        {
            return await Context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await Context.Set<T>().ToListAsync();
        }

        public virtual void Update(T user)
        {
            Context.Set<T>().Update(user);
        }

        public virtual void Add(T user)
        {
            Context.Set<T>().Add(user);
        }

        public virtual void Remove(T user)
        {
            Context.Set<T>().Remove(user);
        }
    }
}
