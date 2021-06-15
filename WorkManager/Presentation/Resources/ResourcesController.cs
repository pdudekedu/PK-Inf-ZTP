using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkManager.Application.Resources;
using WorkManager.Infrastructure.Authorization;

namespace WorkManager.Presentation.Resources
{
    [AuthorizeManager]
    [Route("api/resources")]
    [ApiController]
    public class ResourcesController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ResourcesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet()]
        public async Task<ActionResult<List<ResourceDto>>> GetUsers()
        {
            var users = await _mediator.Send(new GetResourcesQuery());

            return _mapper.Map<List<ResourceDto>>(users);
        }

        [HttpPost()]
        public async Task<ActionResult<ResourceDto>> Create([FromBody] ResourceRequestDto requestDto)
        {
            var command = _mapper.Map<CreateResourceCommand>(requestDto);

            var updated = await _mediator.Send(command);

            return _mapper.Map<ResourceDto>(updated);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ResourceDto>> Update(
            [FromRoute] int id, [FromBody] ResourceRequestDto requestDto)
        {
            var command = _mapper.Map<UpdateResourceCommand>(requestDto);
            command.Id = id;

            var updated = await _mediator.Send(command);

            return _mapper.Map<ResourceDto>(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ResourceDto>> Remove([FromRoute] int id)
        {
            var removedUser = await _mediator.Send(new RemoveResourceCommand { Id = id });

            return _mapper.Map<ResourceDto>(removedUser);
        }
    }
}
