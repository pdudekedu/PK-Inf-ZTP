using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;
using WorkManager.Infrastructure.ErrorHandling.Exceptions;
using WorkManager.Persistence;
using WorkManager.Persistence.Entities;

namespace WorkManager.Application.Teams
{
    public class RemoveTeamCommand : IRequest<Team>
    {
        public int Id { get; set; }
    }

    public class RemoveTeamCommandHandler : IRequestHandler<RemoveTeamCommand, Team>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveTeamCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Team> Handle(RemoveTeamCommand request, CancellationToken cancellationToken)
        {
            var Team = await _unitOfWork.Teams.RemoveAsync(request.Id);

            if(Team == null)
            {
                throw new NotFoundException("Zespół o podanym id nie istnieje");
            }

            await _unitOfWork.CommitAsync();

            return Team;
        }
    }
}
