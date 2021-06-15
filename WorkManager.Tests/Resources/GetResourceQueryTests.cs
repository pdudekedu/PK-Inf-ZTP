using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using WorkManager.Application.Resources;
using WorkManager.Infrastructure.ErrorHandling.Exceptions;
using WorkManager.Persistence;
using WorkManager.Persistence.Entities;
using Xunit;
using Task = System.Threading.Tasks.Task;

namespace WorkManager.Tests.Resources
{
    public class GetResourceQueryTests
    {
        [Fact]
        public async Task Handle_ShouldReturnAllResources()
        {
            //arrange
            var resources = new [] {
                new Resource
                {
                    Id = 1,
                    Name = "Zasób 1",
                    Description = "jakiś opis",
                    InUse = true
                },
                new Resource
                {
                    Id = 2,
                    Name = "Zasób 2",
                    Description = null,
                    InUse = true
                },
                new Resource
                {
                    Id = 3,
                    Name = "Zasób 3",
                    Description = "jakiś inny opis",
                    InUse = true
                }
            };

            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.Resources.GetAllAsync().Returns(new List<Resource>(resources));

            var command = new GetResourcesQuery();
            var handler = new GetResourcesQueryHandler(unitOfWork);

            //act
            var result = await handler.Handle(command);

            //assert
            result.Should().HaveCount(resources.Length).And.Contain(resources);
        }
    }
}
