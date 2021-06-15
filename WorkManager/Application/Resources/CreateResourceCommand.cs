using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;
using WorkManager.Infrastructure.ErrorHandling.Exceptions;
using WorkManager.Persistence;
using WorkManager.Persistence.Entities;

namespace WorkManager.Application.Resources
{
    public class CreateResourceCommand : IRequest<Resource>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class CreateResourceCommandHandler : IRequestHandler<CreateResourceCommand, Resource>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateResourceCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Resource> Handle(CreateResourceCommand request, CancellationToken cancellationToken = default)
        {
            var resource = new Resource
            {
                Name = request.Name,
                Description = request.Description
            };

            _unitOfWork.Resources.Add(resource);

            await _unitOfWork.CommitAsync();

            return resource;
        }
    }
}
