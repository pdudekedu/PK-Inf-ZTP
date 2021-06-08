using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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

        [Authorize]
        [HttpGet("current")]
        public async Task<ActionResult<UserDto>> Login()
        {
            var user = await _mediator.Send(new GetCurrentUserQuery());

            return _mapper.Map<UserDto>(user);
        }

        [HttpGet()]
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
            var users = await _mediator.Send(new GetUsersQuery());

            return _mapper.Map<List<UserDto>>(users);
        }
    }
}
