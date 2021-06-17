using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkManager.Application.Tasks;
using WorkManager.Infrastructure.ErrorHandling.Exceptions;
using WorkManager.Persistence;
using WorkManager.Persistence.Entities;
using Xunit;

namespace WorkManager.Tests.Tasks
{
    public class UpdateTaskStateCommandTests
    {
        [Fact]
        public async System.Threading.Tasks.Task Handle_TaskNotExist_ShoudNotFoundException()
        {
            //arrange
            var unitOfWork = Substitute.For<IUnitOfWork>();

            var command = new UpdateTaskStateCommand()
            {
                ProjectId = 1,
                Id = 1,
            };
            var sut = new UpdateTaskStateCommandHandler(unitOfWork);

            //act
            Func<System.Threading.Tasks.Task> act = async () => await sut.Handle(command, default);

            //assert
            await act.Should().ThrowExactlyAsync<NotFoundException>();
        }

        [Fact]
        public async System.Threading.Tasks.Task Handle_TaskNotExistInProject_ShoudNotFoundException()
        {
            //arrange
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.Tasks.GetByProjectId(1, 1).Returns(default(Task));


            var command = new UpdateTaskStateCommand()
            {
                ProjectId = 2,
                Id = 1,
            };
            var sut = new UpdateTaskStateCommandHandler(unitOfWork);

            //act
            Func<System.Threading.Tasks.Task> act = async () => await sut.Handle(command, default);

            //assert
            await act.Should().ThrowExactlyAsync<NotFoundException>();
        }

        [Theory]
        [InlineData(TaskState.New, TaskState.InProgress)]
        [InlineData(TaskState.InProgress, TaskState.Done)]
        [InlineData(TaskState.InProgress, TaskState.Waiting)]
        [InlineData(TaskState.Waiting, TaskState.InProgress)]
        [InlineData(TaskState.Waiting, TaskState.Done)]
        [InlineData(TaskState.Done, TaskState.InProgress)]
        public async System.Threading.Tasks.Task Handle_TaskExist_ChangeToCorrectState_ShoudReturnTaskWithNewState(TaskState currentState, TaskState newState)
        {
            //arrange
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.Tasks.GetByProjectId(1, 1)
                .Returns(new Task
                {
                    State = currentState,
                    TaskTimes = new List<TaskTime>()
                });


            var command = new UpdateTaskStateCommand()
            {
                ProjectId = 1,
                Id = 1,
                State = newState
            };
            var sut = new UpdateTaskStateCommandHandler(unitOfWork);

            //act
            var result = await sut.Handle(command, default);

            //assert
            result.Should().NotBeNull();
            result.State.Should().Be(newState);
        }

        [Theory]
        [InlineData(TaskState.New, TaskState.New)]
        [InlineData(TaskState.New, TaskState.Waiting)]
        [InlineData(TaskState.New, TaskState.Done)]
        [InlineData(TaskState.InProgress, TaskState.InProgress)]
        [InlineData(TaskState.InProgress, TaskState.New)]
        [InlineData(TaskState.Waiting, TaskState.Waiting)]
        [InlineData(TaskState.Waiting, TaskState.New)]
        [InlineData(TaskState.Done, TaskState.Done)]
        [InlineData(TaskState.Done, TaskState.New)]
        [InlineData(TaskState.Done, TaskState.Waiting)]
        public async System.Threading.Tasks.Task Handle_TaskExist_ChangeToIncorrectState_ShoudThrowConflictException(TaskState currentState, TaskState newState)
        {
            //arrange
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.Tasks.GetByProjectId(1, 1)
                .Returns(new Task
                {
                    State = currentState,
                    TaskTimes = new List<TaskTime>()
                });


            var command = new UpdateTaskStateCommand()
            {
                ProjectId = 1,
                Id = 1,
                State = newState
            };
            var sut = new UpdateTaskStateCommandHandler(unitOfWork);

            //act
            Func<System.Threading.Tasks.Task> act = async () => await sut.Handle(command, default);

            //assert
            await act.Should().ThrowExactlyAsync<ConflictException>();
        }
    }
}
