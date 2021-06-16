using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WorkManager.Infrastructure.Authorization;
using WorkManager.Infrastructure.ErrorHandling.Exceptions;
using WorkManager.Persistence;
using WorkManager.Presentation.Tasks;

namespace WorkManager.Application.Tasks
{
    public class UpdateTaskCommand : IRequest<Persistence.Entities.Task>
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? EstimateStart { get; set; }
        public DateTime? EstimateEnd { get; set; }
        public IEnumerable<ResourceDto> Resources { get; set; }
        public UserDto User { get; set; }
    }

    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, Persistence.Entities.Task>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTaskCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Persistence.Entities.Task> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            //TODO: Walidacja istnienia obiektów
            var Task = await _unitOfWork.Tasks.GetByProjectId(request.ProjectId, request.Id);

            if (Task == null)
            {
                throw new NotFoundException("Zespół o podanym id nie istnieje");
            }

            Task.ProjectId = request.ProjectId;
            Task.Name = request.Name;
            Task.Description = request.Description;
            Task.EstimateStart = request.EstimateStart;
            Task.EstimateEnd = request.EstimateEnd;

            if (request.Resources != null)
            {
                if (Task.Resources != null)
                {
                    var toAdded = await _unitOfWork.Resources.GetByIdsAsync(request.Resources.Where(x => !Task.Resources.Any(y => y.Id == x.Id)).Select(x => x.Id));
                    Task.Resources.AddRange(toAdded);
                    Task.Resources.RemoveAll(x => !request.Resources.Any(y => y.Id == x.Id));
                }
                else
                {
                    Task.Resources = await _unitOfWork.Resources.GetByIdsAsync(request.Resources.Select(x => x.Id));
                }
            }
            if (request.User != null)
            {
                Task.User = await _unitOfWork.Users.GetAsync(request.User.Id);
            }    

            _unitOfWork.Tasks.Update(Task);

            await _unitOfWork.CommitAsync();

            return Task;
        }
    }
}
