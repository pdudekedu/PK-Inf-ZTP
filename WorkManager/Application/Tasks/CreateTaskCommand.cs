using MediatR;
using System.Threading;
using WorkManager.Infrastructure.Authorization;
using WorkManager.Persistence;
using System.Threading.Tasks;
using System;
using WorkManager.Presentation.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace WorkManager.Application.Tasks
{
    public class CreateTaskCommand : IRequest<Persistence.Entities.Task>
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? EstimateStart { get; set; }
        public DateTime? EstimateEnd { get; set; }
        public IEnumerable<ResourceDto> Resources { get; set; }
        public UserDto User { get; set; }
    }

    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Persistence.Entities.Task>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateTaskCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Persistence.Entities.Task> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            //TODO: Walidacja istnienia obiektów
            var Task = new Persistence.Entities.Task
            {
                Name = request.Name,
                Description = request.Description,
                ProjectId = request.ProjectId,
                EstimateStart = request.EstimateStart,
                EstimateEnd = request.EstimateEnd,
                Resources = request.Resources == null ? null : await _unitOfWork.Resources.GetByIdsAsync(request.Resources.Select(x=>x.Id)),
                User = request.User != null ? await _unitOfWork.Users.GetAsync(request.User.Id) : null,
            };

            _unitOfWork.Tasks.Add(Task);

            await _unitOfWork.CommitAsync();

            return Task;
        }
    }
}
