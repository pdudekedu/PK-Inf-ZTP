using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        protected IQueryable<T> InUse => Context.Set<T>().Where(x => x.InUse);

        public virtual async Task<T> GetAsync(int id)
        {
            return await Context.Set<T>().FirstOrDefaultAsync(x => x.Id == id && x.InUse);
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await InUse.ToListAsync();
        }

        public virtual void Update(T entity)
        {
            Context.Set<T>().Update(entity);
        }

        public async virtual Task<T> RemoveAsync(int id)
        {
            var entity = await GetAsync(id);

            if(entity != null)
            {
                entity.InUse = false;
                Context.Set<T>().Update(entity);
            }

            return entity;
        }

        public virtual void Add(T entity)
        {
            Context.Set<T>().Add(entity);
        }

        public virtual void Remove(T entity)
        {
            Context.Set<T>().Remove(entity);
        }
    }
}
