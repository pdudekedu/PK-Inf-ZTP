using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using WorkManager.Infrastructure.Authorization;
using WorkManager.Infrastructure.ErrorHandling.Exceptions;
using WorkManager.Persistence;
using System.Threading.Tasks;
using System;

namespace WorkManager.Application.Tasks
{
    public class CreateTaskCommand : IRequest<Persistence.Entities.Task>
    {
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? EstimateStart { get; set; }
        public DateTime? EstimateEnd { get; set; }
    }

    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Persistence.Entities.Task>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;

        public CreateTaskCommandHandler(IUnitOfWork unitOfWork, IUserContext userContext)
        {
            _unitOfWork = unitOfWork;
            _userContext = userContext;
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
                UserId = _userContext.User.Id
            };

            _unitOfWork.Tasks.Add(Task);

            await _unitOfWork.CommitAsync();

            return Task;
        }
    }
}
