using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkManager.Bussines.Accounts;

namespace WorkManager.Features.Authorization
{
    [Route("api/authorization")]
    [ApiController]
    public class AuthorizationController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AuthorizationController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AccountDto>> Login([FromBody] LoginRequestDto requestDto)
        {
            var account = await _mediator.Send(_mapper.Map<LoginCommand>(requestDto));

            return _mapper.Map<AccountDto>(account);
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterRequestDto requestDto)
        {
            await _mediator.Send(_mapper.Map<RegisterCommand>(requestDto));

            return Ok(new { });
        }
    }
}
