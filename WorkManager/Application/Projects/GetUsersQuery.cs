using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WorkManager.Persistence;
using WorkManager.Persistence.Entities;
using System.Collections.Generic;

namespace WorkManager.Application.Projects
{
    public class GetUsersQuery : IRequest<List<User>>
    {
        public int ProjectId { get; set; }
    }

    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<User>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUsersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Projects.GetUsersForProjectAsync(request.ProjectId);
        }
    }
}
