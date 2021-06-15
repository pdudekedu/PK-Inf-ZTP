using FluentAssertions;
using NSubstitute;
using System.Threading.Tasks;
using WorkManager.Application.Resources;
using WorkManager.Persistence;
using Xunit;

namespace WorkManager.Tests.Resources
{
    public class CreateResourceCommandTests
    {
        [Fact]
        public async Task Handle_WithNameAndDescription_ShouldReturnNewResource()
        {
            //arrange
            var unitOfWork = Substitute.For<IUnitOfWork>();

            var command = new CreateResourceCommand
            {
                Name = "Zasób",
                Description = "opis zasobu"
            };
            var handler = new CreateResourceCommandHandler(unitOfWork);

            //act
            var resource = await handler.Handle(command);

            //assert
            resource.Name.Should().Be(command.Name);
            resource.Description.Should().Be(command.Description);
            resource.InUse.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_WithNameOnly_ShouldReturnNewResource()
        {
            //arrange
            var unitOfWork = Substitute.For<IUnitOfWork>();

            var command = new CreateResourceCommand
            {
                Name = "Zasób",
                Description = null
            };
            var handler = new CreateResourceCommandHandler(unitOfWork);

            //act
            var resource = await handler.Handle(command);

            //assert
            resource.Name.Should().Be(command.Name);
            resource.Description.Should().BeNull();
            resource.InUse.Should().BeTrue();
        }
    }
}
