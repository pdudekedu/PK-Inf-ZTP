using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WorkManager.Persistence;
using System.Collections.Generic;

namespace WorkManager.Application.Tasks
{
    public class GetTasksQuery : IRequest<List<Persistence.Entities.Task>>
    {
        public int ProjectId { get; set; }
    }

    public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, List<Persistence.Entities.Task>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTasksQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Persistence.Entities.Task>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
        {
            var Tasks = await _unitOfWork.Tasks.GetByProjectId(request.ProjectId);

            return Tasks;
        }
    }
}
