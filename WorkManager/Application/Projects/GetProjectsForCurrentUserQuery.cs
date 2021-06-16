using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WorkManager.Persistence;
using WorkManager.Persistence.Entities;
using System.Collections.Generic;
using WorkManager.Infrastructure.Authorization;

namespace WorkManager.Application.Projects
{
    public class GetProjectsForCurrentUserQuery : IRequest<List<Project>>
    {

    }

    public class GetProjectsForCurrentUserQueryHandler : IRequestHandler<GetProjectsForCurrentUserQuery, List<Project>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;

        public GetProjectsForCurrentUserQueryHandler(IUnitOfWork unitOfWork, IUserContext userContext)
        {
            _unitOfWork = unitOfWork;
            _userContext = userContext;
        }

        public async Task<List<Project>> Handle(GetProjectsForCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var projects = await _unitOfWork.Projects.GetAllFor(_userContext.User.Id);

            return projects;
        }
    }
}
