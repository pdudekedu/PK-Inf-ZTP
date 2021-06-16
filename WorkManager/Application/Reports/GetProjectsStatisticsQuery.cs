using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WorkManager.Persistence;
using WorkManager.Persistence.Entities;

namespace WorkManager.Application.Reports
{
    public class GetProjectsStatisticsQuery : IRequest<List<ProjectStatistic>>
    {

    }

    public class GetProjectsStatisticsQueryHandler : IRequestHandler<GetProjectsStatisticsQuery, List<ProjectStatistic>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProjectsStatisticsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ProjectStatistic>> Handle(GetProjectsStatisticsQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.ProjectStatistic.GetAllAsync();
        }
    }
}
