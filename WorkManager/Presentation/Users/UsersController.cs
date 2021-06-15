using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkManager.Application.Users;
using WorkManager.Infrastructure.Authorization;

namespace WorkManager.Presentation.Users
{
    [AuthorizeManager]
    [Route("api/users")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UsersController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet()]
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
            var users = await _mediator.Send(new GetUsersQuery());

            return _mapper.Map<List<UserDto>>(users);
        }

        [HttpPut("{userId:int}")]
        public async Task<ActionResult<UserDto>> UpdateUser(
            [FromRoute] int userId, [FromBody] UpdateUserRequestDto requestDto)
        {
            var command = _mapper.Map<UpdateUserCommand>(requestDto);
            command.Id = userId;

            var updatedUser = await _mediator.Send(command);

            return _mapper.Map<UserDto>(updatedUser);
        }

        [HttpDelete("{userId:int}")]
        public async Task<ActionResult<UserDto>> RemoveUser([FromRoute] int userId)
        {
            var removedUser = await _mediator.Send(new RemoveUserCommand { Id = userId });

            return _mapper.Map<UserDto>(removedUser);
        }
    }
}
