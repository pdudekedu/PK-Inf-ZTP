using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WorkManager.Infrastructure.Authorization;
using WorkManager.Persistence;
using WorkManager.Persistence.Entities;

namespace WorkManager.Application.Projects
{
    public class CreateProjectCommand : IRequest<Project>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int TeamId { get; set; }
        public List<int> Resources { get; set; }
    }

    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Project>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;

        public CreateProjectCommandHandler(IUnitOfWork unitOfWork, IUserContext userContext)
        {
            _unitOfWork = unitOfWork;
            _userContext = userContext;
        }

        public async Task<Project> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            //TODO: Walidacja istnienia obiektów
            var Project = new Project
            {
                Name = request.Name,
                Description = request.Description,
                Resources = request.Resources == null ? null : await _unitOfWork.Resources.GetByIdsAsync(request.Resources),
                Team = await _unitOfWork.Teams.GetAsync(request.TeamId),
                UserId = _userContext.User.Id
            };

            _unitOfWork.Projects.Add(Project);

            await _unitOfWork.CommitAsync();

            return Project;
        }
    }
}
