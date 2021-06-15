using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WorkManager.Persistence;
using WorkManager.Persistence.Entities;
using System.Collections.Generic;

namespace WorkManager.Application.Projects
{
    public class GetProjectsQuery : IRequest<List<Project>>
    {

    }

    public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, List<Project>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProjectsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Project>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
        {
            var Projects = await _unitOfWork.Projects.GetAllAsync();

            return Projects;
        }
    }
}
