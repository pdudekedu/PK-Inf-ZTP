using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkManager.Application.Tasks;
using WorkManager.Infrastructure.Authorization;

namespace WorkManager.Presentation.Tasks
{
    [Authorize]
    [Route("api/projects/{projectId:int}/tasks")]
    [ApiController]
    public class TasksController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public TasksController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet()]
        public async Task<ActionResult<List<TaskDto>>> Get([FromRoute] int projectId)
        {
            var users = await _mediator.Send(new GetTasksQuery() { ProjectId = projectId });

            return _mapper.Map<List<TaskDto>>(users);
        }

        [HttpPost()]
        public async Task<ActionResult<TaskDto>> Create([FromRoute] int projectId, [FromBody] TaskRequestDto requestDto)
        {
            var command = _mapper.Map<CreateTaskCommand>(requestDto);
            command.ProjectId = projectId;

            var updated = await _mediator.Send(command);

            return _mapper.Map<TaskDto>(updated);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<TaskDto>> Update(
            [FromRoute] int projectId, [FromRoute] int id, [FromBody] TaskRequestDto requestDto)
        {
            var command = _mapper.Map<UpdateTaskCommand>(requestDto);
            command.ProjectId = projectId;
            command.Id = id;

            var updated = await _mediator.Send(command);

            return _mapper.Map<TaskDto>(updated);
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult<TaskDto>> UpdateState(
           [FromRoute] int projectId, [FromRoute] int id, [FromBody] TaskStateRequestDto requestDto)
        {
            var command = _mapper.Map<UpdateTaskStateCommand>(requestDto);
            command.ProjectId = projectId;
            command.Id = id;

            var updated = await _mediator.Send(command);

            return _mapper.Map<TaskDto>(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<TaskDto>> Remove([FromRoute] int projectId, [FromRoute] int id)
        {
            var removedUser = await _mediator.Send(new RemoveTaskCommand { ProjectId = projectId, Id = id });

            return _mapper.Map<TaskDto>(removedUser);
        }
    }
}
