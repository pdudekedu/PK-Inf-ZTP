using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;
using WorkManager.Infrastructure.ErrorHandling.Exceptions;
using WorkManager.Persistence;
using WorkManager.Persistence.Entities;

namespace WorkManager.Application.Resources
{
    public class RemoveResourceCommand : IRequest<Resource>
    {
        public int Id { get; set; }
    }

    public class RemoveResourceCommandHandler : IRequestHandler<RemoveResourceCommand, Resource>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveResourceCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Resource> Handle(RemoveResourceCommand request, CancellationToken cancellationToken)
        {
            var resource = await _unitOfWork.Resources.RemoveAsync(request.Id);

            if(resource == null)
            {
                throw new NotFoundException("Zasób o podanym id nie istnieje");
            }

            await _unitOfWork.CommitAsync();

            return resource;
        }
    }
}
