using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;
using WorkManager.Infrastructure.ErrorHandling.Exceptions;
using WorkManager.Persistence;
using WorkManager.Persistence.Entities;

namespace WorkManager.Application.Users
{
    public class RegisterCommand : IRequest<User>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, User>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher<User> _passwordHasher;

        public RegisterCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher<User> passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public async Task<User> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if(await _unitOfWork.Users.ExistsWithUserNameAsync(request.UserName))
            {
                throw new ConflictException("Użytkownik o podanej nazwie już istnieje");
            }

            var user = new User
            {
                UserName = request.UserName,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            user.Password = _passwordHasher.HashPassword(user, request.Password);

            _unitOfWork.Users.Add(user);

            await _unitOfWork.CommitAsync();

            return user;
        }
    }
}
