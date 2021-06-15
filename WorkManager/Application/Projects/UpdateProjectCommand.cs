using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WorkManager.Infrastructure.Authorization;
using WorkManager.Infrastructure.ErrorHandling.Exceptions;
using WorkManager.Persistence;
using WorkManager.Persistence.Entities;

namespace WorkManager.Application.Projects
{
    public class UpdateProjectCommand : IRequest<Project>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TeamId { get; set; }
        public List<int> Resources { get; set; }
    }

    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Project>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;

        public UpdateProjectCommandHandler(IUnitOfWork unitOfWork, IUserContext userContext)
        {
            _unitOfWork = unitOfWork;
            _userContext = userContext;
        }

        public async Task<Project> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            //TODO: Walidacja istnienia obiektów
            var Project = await _unitOfWork.Projects.GetAsync(request.Id);

            if (Project == null)
            {
                throw new NotFoundException("Zespół o podanym id nie istnieje");
            }
            if (request.Resources != null)
            {
                if (Project.Resources != null)
                {
                    var toAdded = await _unitOfWork.Resources.GetByIdsAsync(request.Resources.Where(x => !Project.Resources.Any(y => y.Id == x)));
                    Project.Resources.AddRange(toAdded);
                }
                Project.Resources.RemoveAll(x => !request.Resources.Contains(x.Id));
            }

            Project.Name = request.Name;
            Project.Description = request.Description;
            Project.Team = await _unitOfWork.Teams.GetAsync(request.TeamId);
            Project.UserId = _userContext.User.Id;

            _unitOfWork.Projects.Update(Project);

            await _unitOfWork.CommitAsync();

            return Project;
        }
    }
}
