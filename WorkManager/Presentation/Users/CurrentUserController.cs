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
    [Authorize]
    [Route("api/users/current")]
    [ApiController]
    public class CurrentUserController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CurrentUserController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet()]
        public async Task<ActionResult<UserDto>> Login()
        {
            var user = await _mediator.Send(new GetCurrentUserQuery());

            return _mapper.Map<UserDto>(user);
        }

        [HttpPost("personal-info")]
        public async Task<ActionResult<UserDto>> UpdatePersonalInfo([FromBody] UpdatePersonalInfoRequestDto requestDto)
        {
            var user = await _mediator.Send(_mapper.Map<UpdateCurrentUserPersonalInfoCommand>(requestDto));

            return _mapper.Map<UserDto>(user);
        }

        [HttpPost("password")]
        public async Task<ActionResult<UserDto>> UpdatePersonalInfo([FromBody] UpdatePasswordRequestDto requestDto)
        {
            var user = await _mediator.Send(_mapper.Map<UpdateCurrentUserPasswordCommand>(requestDto));

            return _mapper.Map<UserDto>(user);
        }
    }
}
