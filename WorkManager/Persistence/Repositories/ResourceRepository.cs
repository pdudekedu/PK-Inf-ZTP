using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkManager.Persistence.Entities;

namespace WorkManager.Persistence.Repositories
{
    public interface IResourceRepository : IRepository<Resource>
    {

    }
    public class ResourceRepository : Repository<Resource>, IResourceRepository
    {
        public ResourceRepository(DataContext context) : base(context)
        {
            
        }
    }
}
