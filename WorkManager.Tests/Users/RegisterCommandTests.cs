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
    public class RegisterCommandTests
    {
        [Fact]
        public async Task Handle_UserNotExist_ShoudReturnUser()
        {
            //arrange
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var passwordHasher = new PasswordHasher<User>();

            var command = new RegisterCommand()
            {
                UserName = "User1",
                Password = "User1"
            };
            var sut = new RegisterCommandHandler(unitOfWork, passwordHasher);

            //act
            var user = await sut.Handle(command, default);

            //assert
            user.Should().NotBeNull();
            user.UserName.Should().Be("User1");
            user.Password.Should().NotBe("User1");
        }

        [Fact]
        public async Task Handle_UserExist_ThrowConflictException()
        {
            //arrange
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.Users.ExistsWithUserNameAsync("User1").Returns(true);

            var passwordHasher = new PasswordHasher<User>();

            var command = new RegisterCommand()
            {
                UserName = "User1",
                Password = "User1"
            };
            var sut = new RegisterCommandHandler(unitOfWork, passwordHasher);

            //act
            Func<Task> act = async () => await sut.Handle(command, default);

            //assert
            await act.Should().ThrowExactlyAsync<ConflictException>();
        }
    }
}
