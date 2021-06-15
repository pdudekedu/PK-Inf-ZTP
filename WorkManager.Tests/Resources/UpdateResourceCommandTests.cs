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
    public class UpdateResourceCommandTests
    {
        [Fact]
        public async Task Handle_WithChangedName_ShouldReturnUpdatedResource()
        {
            //arrange
            var resource = new Resource
            {
                Id = 1,
                Name = "Zasób",
                Description = "jakiś opis",
                InUse = true
            };

            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.Resources.GetAsync(1).Returns(resource);

            var command = new UpdateResourceCommand
            {
                Id = 1,
                Name = "Zasób 111",
                Description = resource.Description
            };
            var handler = new UpdateResourceCommandHandler(unitOfWork);

            //act
            var updated = await handler.Handle(command);

            //assert
            updated.Name.Should().Be(command.Name);
            updated.Description.Should().Be(resource.Description);
            updated.InUse.Should().BeTrue();
        }

        [Theory]
        [InlineData("jakiś opis", "inny opis")]
        [InlineData("jakiś opis", null)]
        [InlineData(null, "jakiś opis")]
        public async Task Handle_WithChangedDescription_ShouldReturnUpdatedResource(string from, string to)
        {
            //arrange
            var resource = new Resource
            {
                Id = 1,
                Name = "Zasób",
                Description = from,
                InUse = true
            };

            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.Resources.GetAsync(1).Returns(resource);

            var command = new UpdateResourceCommand
            {
                Id = 1,
                Name = resource.Name,
                Description = to
            };
            var handler = new UpdateResourceCommandHandler(unitOfWork);

            //act
            var updated = await handler.Handle(command);

            //assert
            updated.Name.Should().Be(resource.Name);
            updated.Description.Should().Be(to);
            updated.InUse.Should().BeTrue();
        }

        [Fact]
        public void Handle_ForNotExisingResource_ShouldThrowAnException()
        {
            //arrange
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.Resources.GetAsync(1).Returns(default(Resource));

            var command = new UpdateResourceCommand
            {
                Id = 1,
                Name = "Zasób 111",
                Description = "Jakiś opis"
            };
            var handler = new UpdateResourceCommandHandler(unitOfWork);

            //act
            Func<Task> act = async () => await handler.Handle(command);

            //assert
            act.Should().ThrowExactly<NotFoundException>();
        }
    }
}
