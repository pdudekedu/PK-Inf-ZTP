﻿using MediatR;
using System.Threading;
using WorkManager.Infrastructure.Authorization;
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
                UserId = request.UserId
            };

            _unitOfWork.Tasks.Add(Task);

            await _unitOfWork.CommitAsync();

            return Task;
        }
    }
}
