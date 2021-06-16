using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkManager.Application.Reports;
using WorkManager.Infrastructure.Authorization;

namespace WorkManager.Presentation.Reports
{
    [AuthorizeManager]
    [Route("api/reports")]
    [ApiController]
    public class ReportsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ReportsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("projects")]
        public async Task<ActionResult<List<ProjectStatisticDto>>> GetProjectStatistics()
        {
            var users = await _mediator.Send(new GetProjectsStatisticsQuery());

            return _mapper.Map<List<ProjectStatisticDto>>(users);
        }

        [HttpGet("users")]
        public async Task<ActionResult<List<UserStatisticDto>>> GetUserStatistics()
        {
            var users = await _mediator.Send(new GetUsersStatisticsQuery());

            return _mapper.Map<List<UserStatisticDto>>(users);
        }
    }
}
