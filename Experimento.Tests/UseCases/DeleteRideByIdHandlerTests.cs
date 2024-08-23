using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Experimento.Application.Services.Interfaces;
using Experimento.Application.UseCases.DeleteRideById;
using Experimento.Domain.Entities;
using Experimento.Domain.Interfaces;
using Experimento.Domain.Notification;
using FluentAssertions;
using Moq;
using Xunit;

namespace Experimento.Tests.UseCases
{
    public class DeleteRideByIdHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ICheckIfRideExistsService> _checkIfRideExistsServiceMock;
        private readonly Mock<IRideRepository> _rideRepositoryMock;
        private readonly Mock<NotificationContext> _notificationContextMock;
        private readonly DeleteRideByIdHandler _handler;

        public DeleteRideByIdHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _checkIfRideExistsServiceMock = new Mock<ICheckIfRideExistsService>();
            _rideRepositoryMock = new Mock<IRideRepository>();
            _notificationContextMock = new Mock<NotificationContext>();

            _handler = new DeleteRideByIdHandler(
                _unitOfWorkMock.Object,
                _mapperMock.Object,
                _checkIfRideExistsServiceMock.Object,
                _rideRepositoryMock.Object,
                _notificationContextMock.Object
            );
        }

        [Fact]
        public async Task Handle_ShouldReturnDeleteRideByIdResult_WhenRideExists()
        {
            // Arrange
            var command = new DeleteRideByIdCommand
            {
                RideId = "ride9"
            };
            var existentRide = new Ride();
            var deleteRideByIdResult = new DeleteRideByIdResult();

            SetupRideExistsCheck(existentRide);
            SetupNotificationContext(false);
            SetupMapper(existentRide, deleteRideByIdResult);
            SetupRideRepository(existentRide);
            SetupUnitOfWork();

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be(deleteRideByIdResult);
        }

        [Fact]
        public async Task Handle_ShouldReturnDeleteRideByIdResult_WhenRideDoesNotExist()
        {
            // Arrange
            var command = new DeleteRideByIdCommand
            {
                RideId = "ride9"
            };
            SetupRideExistsCheck(null);
            SetupNotificationContext(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeOfType<DeleteRideByIdResult>();
        }

        [Fact]
        public async Task Handle_ShouldReturnDeleteRideByIdResult_WhenThereAreNotifications()
        {
            // Arrange
            var command = new DeleteRideByIdCommand
            {
                RideId = "ride9"
            };
            var existentRide = new Ride();
            SetupRideExistsCheck(existentRide);
            SetupNotificationContext(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeOfType<DeleteRideByIdResult>();
        }

        private void SetupRideExistsCheck(Ride ride) =>
            _checkIfRideExistsServiceMock
                .Setup(s => s.Check(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(ride);

        private void SetupNotificationContext(bool hasNotifications) =>
            _notificationContextMock
                .Setup(nc => nc.HasNotifications())
                .Returns(hasNotifications);

        private void SetupMapper(Ride ride, DeleteRideByIdResult result) =>
            _mapperMock
                .Setup(m => m.Map<DeleteRideByIdResult>(ride))
                .Returns(result);

        private void SetupRideRepository(Ride ride) =>
            _rideRepositoryMock
                .Setup(r => r.DeleteRide(ride, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

        private void SetupUnitOfWork() =>
            _unitOfWorkMock
                .Setup(u => u.Commit(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
    }
}
