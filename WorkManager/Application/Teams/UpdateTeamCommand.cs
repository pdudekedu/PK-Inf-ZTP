using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WorkManager.Infrastructure.ErrorHandling.Exceptions;
using WorkManager.Persistence;
using WorkManager.Persistence.Entities;

namespace WorkManager.Application.Teams
{
    public class UpdateTeamCommand : IRequest<Team>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<int> Users { get; set; }
    }

    public class UpdateTeamCommandHandler : IRequestHandler<UpdateTeamCommand, Team>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTeamCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Team> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
        {
            var Team = await _unitOfWork.Teams.GetAsync(request.Id);

            if (Team == null)
            {
                throw new NotFoundException("Zespół o podanym id nie istnieje");
            }

            var toAdded = await _unitOfWork.Users.GetUsersById(request.Users.Where(x => !Team.Users.Any(y => y.Id == x)));

            Team.Name = request.Name;
            Team.Description = request.Description;
            Team.Users.AddRange(toAdded);
            Team.Users.RemoveAll(x=> !request.Users.Contains(x.Id));

            _unitOfWork.Teams.Update(Team);

            await _unitOfWork.CommitAsync();

            return Team;
        }
    }
}
