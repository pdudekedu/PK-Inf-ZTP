using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WorkManager.Persistence;
using WorkManager.Persistence.Entities;
using System.Collections.Generic;

namespace WorkManager.Application.Teams
{
    public class GetTeamsQuery : IRequest<List<Team>>
    {

    }

    public class GetTeamsQueryHandler : IRequestHandler<GetTeamsQuery, List<Team>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTeamsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Team>> Handle(GetTeamsQuery request, CancellationToken cancellationToken)
        {
            var teams = await _unitOfWork.Teams.GetAllAsync();

            return teams;
        }
    }
}
