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
    public class LoginCommand : IRequest<Account>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, Account>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher<Account> _passwordHasher;

        public LoginCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher<Account> passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public async Task<Account> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var account = await _unitOfWork.Accounts.GetByUserNameAsync(request.UserName);

            if(account == null 
                || _passwordHasher.VerifyHashedPassword(account, account.Password, request.Password) 
                    == PasswordVerificationResult.Failed)
            {
                throw new UnauthorizedException("Niepoprawny login lub hasło");
            }

            return account;
        }
    }
}
