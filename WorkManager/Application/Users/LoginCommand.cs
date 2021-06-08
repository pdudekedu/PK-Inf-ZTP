using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;
using WorkManager.Persistence;
using WorkManager.Persistence.Entities;
using WorkManager.Infrastructure.ErrorHandling.Exceptions;

namespace WorkManager.Application.Users
{
    public class LoginCommand : IRequest<User>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, User>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher<User> _passwordHasher;

        public LoginCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher<User> passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public async Task<User> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByUserNameAsync(request.UserName);

            if (user == null
                || _passwordHasher.VerifyHashedPassword(user, user.Password, request.Password)
                    == PasswordVerificationResult.Failed)
            {
                throw new UnauthorizedException("Niepoprawna nazwa użytkownika lub hasło");
            }

            return user;
        }
    }
}
