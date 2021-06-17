using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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
        private readonly Dictionary<TaskState, List<TaskState>> allowState = new()
        {
            { TaskState.New, new() { TaskState.InProgress } },
            { TaskState.InProgress, new() { TaskState.Waiting, TaskState.Done } },
            { TaskState.Waiting, new() { TaskState.InProgress, TaskState.Done } },
            { TaskState.Done, new() { TaskState.InProgress } }
        };

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
                throw new NotFoundException("Zadanie o podanym id nie istnieje w tym projekcie.");
            }

            if (!allowState[Task.State].Contains(request.State))
            {
                throw new ConflictException("Niepoprawna próba zmiany statusu.");
            }

            Task.TaskTimes.Add(new TaskTime()
            {
                DateTime = DateTime.Now,
                Type = (Task.State, request.State) switch
                {
                    (TaskState.New, TaskState.InProgress) => TaskTimeType.Start,
                    (TaskState.InProgress, TaskState.Waiting) => TaskTimeType.Suspend,
                    (TaskState.Waiting, TaskState.InProgress) => TaskTimeType.Resume,
                    (TaskState.Done, TaskState.InProgress) => TaskTimeType.Resume,
                    (TaskState.Waiting, TaskState.Done) => TaskTimeType.End,
                    (TaskState.InProgress, TaskState.Done) => TaskTimeType.End,
                    _ => TaskTimeType.Start
                }
            });
            Task.State = request.State;
            _unitOfWork.Tasks.Update(Task);

            await _unitOfWork.CommitAsync();

            return Task;
        }
    }
}
