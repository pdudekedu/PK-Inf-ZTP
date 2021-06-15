using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;
using WorkManager.Infrastructure.Authorization;
using WorkManager.Infrastructure.ErrorHandling.Exceptions;
using WorkManager.Persistence;

namespace WorkManager.Application.Tasks
{
    public class RemoveTaskCommand : IRequest<Persistence.Entities.Task>
    {
        public int ProjectId { get; set; }
        public int Id { get; set; }
    }

    public class RemoveTaskCommandHandler : IRequestHandler<RemoveTaskCommand, Persistence.Entities.Task>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveTaskCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Persistence.Entities.Task> Handle(RemoveTaskCommand request, CancellationToken cancellationToken)
        {
            var Task = await _unitOfWork.Tasks.RemoveAsync(request.Id);

            if(Task == null)
            {
                throw new NotFoundException("Zespół o podanym id nie istnieje");
            }

            await _unitOfWork.CommitAsync();

            return Task;
        }
    }
}
