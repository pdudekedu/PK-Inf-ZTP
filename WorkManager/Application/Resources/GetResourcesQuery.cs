using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WorkManager.Persistence;
using WorkManager.Persistence.Entities;
using System.Collections.Generic;

namespace WorkManager.Application.Resources
{
    public class GetResourcesQuery : IRequest<List<Resource>>
    {

    }

    public class GetResourcesQueryHandler : IRequestHandler<GetResourcesQuery, List<Resource>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetResourcesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Resource>> Handle(GetResourcesQuery request, CancellationToken cancellationToken)
        {
            var resources = await _unitOfWork.Resources.GetAllAsync();

            return resources;
        }
    }
}
