using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WorkManager.Application.Users;
using WorkManager.Infrastructure.Authorization;
using WorkManager.Persistence.Entities;

namespace WorkManager.Presentation.Authorization
{
    [Route("api/authorization")]
    [ApiController]
    public class AuthorizationController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly JwtConfig _jwtConfig;

        public AuthorizationController(IMediator mediator, IMapper mapper, IOptions<JwtConfig> jwtConfig)
        {
            _mediator = mediator;
            _mapper = mapper;
            _jwtConfig = jwtConfig.Value;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto requestDto)
        {
            var user = await _mediator.Send(_mapper.Map<LoginCommand>(requestDto));

            string token = GenerateJwtToken(user);

            HttpContext.Response.Cookies.Append("auth", token, new CookieOptions
            {
                MaxAge = new TimeSpan(7, 0, 0, 0),
                Secure = true
            });

            return new LoginResponseDto
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role,
                Token = token
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterRequestDto requestDto)
        {
            await _mediator.Send(_mapper.Map<RegisterCommand>(requestDto));

            return Ok(new { });
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
