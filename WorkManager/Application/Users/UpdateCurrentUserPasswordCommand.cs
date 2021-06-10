using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;
using WorkManager.Infrastructure.Authorization;
using WorkManager.Infrastructure.ErrorHandling.Exceptions;
using WorkManager.Persistence;
using WorkManager.Persistence.Entities;

namespace WorkManager.Application.Users
{
    public class UpdateCurrentUserPasswordCommand : IRequest<User>
    {
        public string OldPassword { get; set; }
        public string Password { get; set; }
    }

    public class UpdateCurrentUserPasswordCommandHandler : IRequestHandler<UpdateCurrentUserPasswordCommand, User>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UpdateCurrentUserPasswordCommandHandler(IUnitOfWork unitOfWork, IUserContext userContext, IPasswordHasher<User> passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _userContext = userContext;
            _passwordHasher = passwordHasher;
        }

        public async Task<User> Handle(UpdateCurrentUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetAsync(_userContext.User.Id);

            if (_passwordHasher.VerifyHashedPassword(user, user.Password, request.OldPassword) 
                == PasswordVerificationResult.Failed)
            {
                throw new ValidationException("Niepoprawne hasło");
            }

            user.Password = _passwordHasher.HashPassword(user, request.Password);

            _unitOfWork.Users.UpdateAsync(user);

            await _unitOfWork.CommitAsync();

            return user;
        }
    }
}
