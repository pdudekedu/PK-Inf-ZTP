using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WorkManager.Infrastructure.ErrorHandling.Exceptions;
using WorkManager.Persistence;
using WorkManager.Persistence.Entities;

namespace WorkManager.Application.Users
{
    public class RemoveUserCommand : IRequest<User>
    {
        public int Id { get; set; }
    }

    public class RemoveUserCommandHandler : IRequestHandler<RemoveUserCommand, User>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher<User> _passwordHasher;

        public RemoveUserCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher<User> passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public async Task<User> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetAsync(request.Id);

            if(user == null)
            {
                throw new NotFoundException($"Użytkownik o id ${request.Id} nie istnieje");
            }

            _unitOfWork.Users.Remove(user);

            await _unitOfWork.CommitAsync();

            return user;
        }
    }
}
