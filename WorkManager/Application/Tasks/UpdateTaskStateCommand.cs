using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WorkManager.Infrastructure.Authorization;
using WorkManager.Infrastructure.ErrorHandling.Exceptions;
using WorkManager.Persistence;
using WorkManager.Persistence.Entities;

namespace WorkManager.Application.Tasks
{
    public class UpdateTaskStateCommand : IRequest<Persistence.Entities.Task>
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public TaskState State { get; set; }
    }

    public class UpdateTaskStateCommandHandler : IRequestHandler<UpdateTaskStateCommand, Persistence.Entities.Task>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTaskStateCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Persistence.Entities.Task> Handle(UpdateTaskStateCommand request, CancellationToken cancellationToken)
        {
            //TODO: Walidacja istnienia obiektów
            var Task = await _unitOfWork.Tasks.GetByProjectId(request.ProjectId, request.Id);

            if (Task == null)
            {
                throw new NotFoundException("Zespół o podanym id nie istnieje");
            }

            Task.State = request.State;

            _unitOfWork.Tasks.Update(Task);

            await _unitOfWork.CommitAsync();

            return Task;
        }
    }
}
