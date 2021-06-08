using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkManager.Infrastructure.Authorization;

namespace WorkManager.Presentation.Board
{
    [Authorize]
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
        public async Task<ActionResult<List<ProjectDto>>> GetProjects()
        {
            return new List<ProjectDto>
            {
                new ProjectDto { Id = 1, Name = "Projekt 1" },
                new ProjectDto { Id = 2, Name = "Projekt 1" },
                new ProjectDto { Id = 3, Name = "Projekt 1" },
            };
        }
    }
}
