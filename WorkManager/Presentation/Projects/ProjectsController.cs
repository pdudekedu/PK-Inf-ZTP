using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkManager.Application.Projects;
using WorkManager.Infrastructure.Authorization;

namespace WorkManager.Presentation.Projects
{
    [AuthorizeManager]
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProjectsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet()]
        public async Task<ActionResult<List<ProjectDto>>> GetUsers()
        {
            var users = await _mediator.Send(new GetProjectsQuery());

            return _mapper.Map<List<ProjectDto>>(users);
        }

        [HttpPost()]
        public async Task<ActionResult<ProjectDto>> Create([FromBody] ProjectRequestDto requestDto)
        {
            var command = _mapper.Map<CreateProjectCommand>(requestDto);

            var updated = await _mediator.Send(command);

            return _mapper.Map<ProjectDto>(updated);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ProjectDto>> Update(
            [FromRoute] int id, [FromBody] ProjectRequestDto requestDto)
        {
            var command = _mapper.Map<UpdateProjectCommand>(requestDto);
            command.Id = id;

            var updated = await _mediator.Send(command);

            return _mapper.Map<ProjectDto>(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProjectDto>> Remove([FromRoute] int id)
        {
            var removedUser = await _mediator.Send(new RemoveProjectCommand { Id = id });

            return _mapper.Map<ProjectDto>(removedUser);
        }
    }
}
