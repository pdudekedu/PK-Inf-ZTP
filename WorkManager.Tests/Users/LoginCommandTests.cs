using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkManager.Application.Users;
using WorkManager.Infrastructure.ErrorHandling.Exceptions;
using WorkManager.Persistence;
using Xunit;
using User = WorkManager.Persistence.Entities.User;

namespace WorkManager.Tests.Users
{
    public class LoginCommandTests
    {
        [Fact]
        public async Task Handle_UserNotExist_ShoudThrowUnauthorizedException()
        {
            //arrange
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var passwordHasher = Substitute.For<IPasswordHasher<User>>();

            var command = new LoginCommand()
            {
                UserName = "User1",
                Password = "User1"
            };
            var sut = new LoginCommandHandler(unitOfWork, passwordHasher);

            //act
            Func<Task> act = async () => await sut.Handle(command, default);

            //assert
            await act.Should().ThrowExactlyAsync<UnauthorizedException>();
        }

        [Fact]
        public async Task Handle_IncorrectPassword_ShoudThrowUnauthorizedException()
        {
            //arrange
            var passwordHasher = new PasswordHasher<User>();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.Users.GetByUserNameAsync("User1")
                .Returns(new User
                {
                    UserName = "User1",
                    Password = passwordHasher.HashPassword(default, "User1"),
                });


            var command = new LoginCommand()
            {
                UserName = "User1",
                Password = "User2"
            };
            var sut = new LoginCommandHandler(unitOfWork, passwordHasher);

            //act
            Func<Task> act = async () => await sut.Handle(command, default);

            //assert
            await act.Should().ThrowExactlyAsync<UnauthorizedException>();
        }

        [Fact]
        public async Task Handle_CorrectCredentials_ShoudReturnUser()
        {
            //arrange
            var passwordHasher = new PasswordHasher<User>();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.Users.GetByUserNameAsync("User1")
                .Returns(new User
                {
                    UserName = "User1",
                    Password = passwordHasher.HashPassword(default, "User1"),
                });


            var command = new LoginCommand()
            {
                UserName = "User1",
                Password = "User1"
            };
            var sut = new LoginCommandHandler(unitOfWork, passwordHasher);

            //act
            var user = await sut.Handle(command, default);

            //assert
            user.Should().NotBeNull();
            user.UserName.Should().Be("User1");
        }
    }
}
