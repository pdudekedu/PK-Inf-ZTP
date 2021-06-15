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
