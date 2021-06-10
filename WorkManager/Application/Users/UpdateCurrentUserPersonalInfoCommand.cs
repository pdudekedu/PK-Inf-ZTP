using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WorkManager.Infrastructure.Authorization;
using WorkManager.Persistence;
using WorkManager.Persistence.Entities;

namespace WorkManager.Application.Users
{
    public class UpdateCurrentUserPersonalInfoCommand : IRequest<User>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class UpdateCurrentUserPersonalInfoCommandHandler : IRequestHandler<UpdateCurrentUserPersonalInfoCommand, User>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;

        public UpdateCurrentUserPersonalInfoCommandHandler(IUnitOfWork unitOfWork, IUserContext userContext)
        {
            _unitOfWork = unitOfWork;
            _userContext = userContext;
        }

        public async Task<User> Handle(UpdateCurrentUserPersonalInfoCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetAsync(_userContext.User.Id);

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;

            _unitOfWork.Users.UpdateAsync(user);

            await _unitOfWork.CommitAsync();

            return user;
        }
    }
}
