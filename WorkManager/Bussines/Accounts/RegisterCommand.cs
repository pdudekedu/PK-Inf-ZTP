using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WorkManager.Data;
using WorkManager.Data.Entities;
using WorkManager.ErrorHandling.Exceptions;

namespace WorkManager.Bussines.Accounts
{
    public class RegisterCommand : IRequest<Account>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Account>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher<Account> _passwordHasher;

        public RegisterCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher<Account> passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public async Task<Account> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var account = new Account
            {
                UserName = request.UserName,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            account.Password = _passwordHasher.HashPassword(account, request.Password);

            _unitOfWork.Accounts.AddAsync(account);

            await _unitOfWork.Commit();

            return account;
        }
    }
}
