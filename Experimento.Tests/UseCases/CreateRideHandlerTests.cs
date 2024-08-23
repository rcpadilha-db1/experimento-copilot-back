using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Experimento.Application.Services.Interfaces;
using Experimento.Application.UseCases.CreateRide;
using Experimento.Application.UseCases.CreateRideByUserId;
using Experimento.Domain.Entities;
using Experimento.Domain.Interfaces;
using Experimento.Domain.Notification;
using FluentAssertions;
using Moq;
using Xunit;

namespace Experimento.Tests.UseCases
{
    public class CreateRideHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ICheckRideRequirementsService> _checkRideRequirementsServiceMock;
        private readonly Mock<IRideRepository> _rideRepositoryMock;
        private readonly Mock<NotificationContext> _notificationContextMock;
        private readonly CreateRideHandler _handler;

        public CreateRideHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _checkRideRequirementsServiceMock = new Mock<ICheckRideRequirementsService>();
            _rideRepositoryMock = new Mock<IRideRepository>();
            _notificationContextMock = new Mock<NotificationContext>();

            _handler = new CreateRideHandler(
                _unitOfWorkMock.Object,
                _mapperMock.Object,
                _checkRideRequirementsServiceMock.Object,
                _rideRepositoryMock.Object,
                _notificationContextMock.Object
            );
        }

        [Fact]
        public async Task Handle_ShouldReturnCreateRideResult_WhenRequirementsAreMet()
        {
            // Arrange
            var command = CreateCommand();
            var ride = new Ride();
            var createRideResult = new CreateRideResult();

            SetupSuccessfulRequirementsCheck();
            SetupNotificationContext(false);
            SetupMapper(command, ride, createRideResult);
            SetupRideRepository(ride);
            SetupUnitOfWork();

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be(createRideResult);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenRequirementsAreNotMet()
        {
            // Arrange
            var command = CreateCommand();
            SetupSuccessfulRequirementsCheck();
            SetupNotificationContext(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }

        private static CreateRideCommand CreateCommand() =>
            new()
            {
                Id = "ride9",
                RiderId = "userId",
                VehicleId = "vehicleId",
                Date = DateTime.UtcNow
            };

        private void SetupSuccessfulRequirementsCheck() =>
            _checkRideRequirementsServiceMock
                .Setup(s => s.CheckIfAreRequirementsToRide(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

        private void SetupNotificationContext(bool hasNotifications) =>
            _notificationContextMock
                .Setup(nc => nc.HasNotifications())
                .Returns(hasNotifications);

        private void SetupMapper(CreateRideCommand command, Ride ride, CreateRideResult createRideResult)
        {
            _mapperMock
                .Setup(m => m.Map<Ride>(command))
                .Returns(ride);
            _mapperMock
                .Setup(m => m.Map<CreateRideResult>(ride))
                .Returns(createRideResult);
        }

        private void SetupRideRepository(Ride ride) =>
            _rideRepositoryMock
                .Setup(r => r.CreateRideAsync(ride, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

        private void SetupUnitOfWork() =>
            _unitOfWorkMock
                .Setup(u => u.Commit(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
    }
}
