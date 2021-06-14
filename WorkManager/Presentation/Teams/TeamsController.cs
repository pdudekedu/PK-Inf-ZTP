using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkManager.Application.Teams;
using WorkManager.Infrastructure.Authorization;

namespace WorkManager.Presentation.Teams
{
    [AuthorizeManager]
    [Route("api/teams")]
    [ApiController]
    public class TeamsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public TeamsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet()]
        public async Task<ActionResult<List<TeamDto>>> GetUsers()
        {
            var users = await _mediator.Send(new GetTeamsQuery());

            return _mapper.Map<List<TeamDto>>(users);
        }

        [HttpPost()]
        public async Task<ActionResult<TeamDto>> Create([FromBody] TeamRequestDto requestDto)
        {
            var command = _mapper.Map<CreateTeamCommand>(requestDto);

            var updated = await _mediator.Send(command);

            return _mapper.Map<TeamDto>(updated);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<TeamDto>> Update(
            [FromRoute] int id, [FromBody] TeamRequestDto requestDto)
        {
            var command = _mapper.Map<UpdateTeamCommand>(requestDto);
            command.Id = id;

            var updated = await _mediator.Send(command);

            return _mapper.Map<TeamDto>(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<TeamDto>> Remove([FromRoute] int id)
        {
            var removedUser = await _mediator.Send(new RemoveTeamCommand { Id = id });

            return _mapper.Map<TeamDto>(removedUser);
        }
    }
}
