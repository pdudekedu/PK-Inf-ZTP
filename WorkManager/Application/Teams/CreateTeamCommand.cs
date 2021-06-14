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
    public class CreateTeamCommand : IRequest<Team>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<int> Users { get; set; }
    }

    public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, Team>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateTeamCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Team> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
        {
            if (!await _unitOfWork.Users.ExistsUsersAsync(request.Users))
            {
                throw new ConflictException("Wystąpił problem podczas dodwania użytkowników do zespołu.");
            }

            var team = new Team
            {
                Name = request.Name,
                Description = request.Description,
                Users = await _unitOfWork.Users.GetUsersById(request.Users)
            };

            _unitOfWork.Teams.Add(team);

            await _unitOfWork.CommitAsync();

            return team;
        }
    }
}
