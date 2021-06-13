using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;
using WorkManager.Infrastructure.ErrorHandling.Exceptions;
using WorkManager.Persistence;
using WorkManager.Persistence.Entities;

namespace WorkManager.Application.Resources
{
    public class UpdateResourceCommand : IRequest<Resource>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class UpdateResourceCommandHandler : IRequestHandler<UpdateResourceCommand, Resource>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateResourceCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Resource> Handle(UpdateResourceCommand request, CancellationToken cancellationToken)
        {
            var resource = await _unitOfWork.Resources.GetAsync(request.Id);

            if (resource == null)
            {
                throw new NotFoundException("Zasób o podanym id nie istnieje");
            }

            resource.Name = request.Name;
            resource.Description = request.Description;

            _unitOfWork.Resources.Update(resource);

            await _unitOfWork.CommitAsync();

            return resource;
        }
    }
}
