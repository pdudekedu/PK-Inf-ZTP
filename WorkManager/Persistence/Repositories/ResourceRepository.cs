using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkManager.Persistence.Entities;

namespace WorkManager.Persistence.Repositories
{
    public class ResourceRepository : Repository<Resource>
    {
        public ResourceRepository(DataContext context) : base(context)
        {
            
        }
    }
}
