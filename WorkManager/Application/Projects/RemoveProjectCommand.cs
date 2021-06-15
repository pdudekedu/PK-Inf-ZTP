using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WorkManager.Infrastructure.ErrorHandling.Exceptions;
using WorkManager.Persistence;
using WorkManager.Persistence.Entities;

namespace WorkManager.Application.Projects
{
    public class RemoveProjectCommand : IRequest<Project>
    {
        public int Id { get; set; }
    }

    public class RemoveProjectCommandHandler : IRequestHandler<RemoveProjectCommand, Project>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveProjectCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Project> Handle(RemoveProjectCommand request, CancellationToken cancellationToken)
        {
            var Project = await _unitOfWork.Projects.RemoveAsync(request.Id);

            if(Project == null)
            {
                throw new NotFoundException("Zespół o podanym id nie istnieje");
            }

            await _unitOfWork.CommitAsync();

            return Project;
        }
    }
}
