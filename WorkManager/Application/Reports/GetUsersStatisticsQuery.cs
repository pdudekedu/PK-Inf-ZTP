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
    public class GetUsersStatisticsQuery : IRequest<List<UserStatistic>>
    {

    }

    public class GetUsersStatisticsQueryQueryHandler : IRequestHandler<GetUsersStatisticsQuery, List<UserStatistic>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUsersStatisticsQueryQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<UserStatistic>> Handle(GetUsersStatisticsQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.UserStatistic.GetAllAsync();
        }
    }
}
