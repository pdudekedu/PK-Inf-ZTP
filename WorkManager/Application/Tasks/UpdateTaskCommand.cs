using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WorkManager.Infrastructure.Authorization;
using WorkManager.Infrastructure.ErrorHandling.Exceptions;
using WorkManager.Persistence;

namespace WorkManager.Application.Tasks
{
    public class UpdateTaskCommand : IRequest<Persistence.Entities.Task>
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? EstimateStart { get; set; }
        public DateTime? EstimateEnd { get; set; }
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
            Task.UserId = request.UserId;
            Task.EstimateStart = request.EstimateStart;
            Task.EstimateEnd = request.EstimateEnd;

            _unitOfWork.Tasks.Update(Task);

            await _unitOfWork.CommitAsync();

            return Task;
        }
    }
}
