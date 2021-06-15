using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkManager.Application.Resources;
using WorkManager.Infrastructure.ErrorHandling.Exceptions;
using WorkManager.Persistence;
using WorkManager.Persistence.Entities;
using WorkManager.Persistence.Repositories;
using Xunit;

namespace WorkManager.Tests.Resources
{
    public class RemoveResourceCommandTests
    {
        [Fact]
        public void Handle_ForNotExisingResource_ShouldThrowAnException()
        {
            //arrange
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.Resources.RemoveAsync(1).Returns(default(Resource));

            var command = new RemoveResourceCommand
            {
                Id = 1
            };
            var handler = new RemoveResourceCommandHandler(unitOfWork);

            //act
            Func<Task> act = async () => await handler.Handle(command);

            //assert
            act.Should().ThrowExactly<NotFoundException>();
        }
    }
}
