using System.Threading;
using System.Threading.Tasks;
using Experimento.Application.Services;
using Experimento.Domain.Entities;
using Experimento.Domain.Interfaces;
using Experimento.Domain.Notification;
using FluentAssertions;
using Moq;
using Xunit;

namespace Experimento.Tests.Services
{
    public class CheckIfRideExistsServiceTests
    {
        private readonly Mock<IRideRepository> _rideRepositoryMock;
        private readonly Mock<NotificationContext> _notificationContextMock;
        private readonly CheckIfRideExistsService _service;

        public CheckIfRideExistsServiceTests()
        {
            _rideRepositoryMock = new Mock<IRideRepository>();
            _notificationContextMock = new Mock<NotificationContext>();

            _service = new CheckIfRideExistsService(
                _rideRepositoryMock.Object,
                _notificationContextMock.Object
            );
        }

        [Fact]
        public async Task Check_ShouldReturnRide_WhenRideExists()
        {
            // Arrange
            var rideId = "ride9";
            var existentRide = new Ride();
            SetupRideRepositoryToReturnRide(rideId, existentRide);
            SetupNotificationContextToNotAddNotification();

            // Act
            var result = await _service.Check(rideId, CancellationToken.None);

            // Assert
            result.Should().Be(existentRide);
            VerifyNotificationContextNotCalled();
        }

        [Fact]
        public async Task Check_ShouldReturnNull_WhenRideDoesNotExist()
        {
            // Arrange
            var rideId = "ride9";
            SetupRideRepositoryToReturnNull(rideId);
            SetupNotificationContextToAddNotFoundNotification();

            // Act
            var result = await _service.Check(rideId, CancellationToken.None);

            // Assert
            result.Should().BeNull();
            VerifyNotificationContextCalledWithNotFound();
        }

        private void SetupRideRepositoryToReturnRide(string rideId, Ride ride)
        {
            _rideRepositoryMock
                .Setup(r => r.ListRideById(rideId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(ride);
        }

        private void SetupRideRepositoryToReturnNull(string rideId)
        {
            _rideRepositoryMock
                .Setup(r => r.ListRideById(rideId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Ride?)null);
        }

        private void SetupNotificationContextToNotAddNotification()
        {
            _notificationContextMock
                .Setup(nc => nc.AddNotification(It.IsAny<string>()))
                .Verifiable();
        }

        private void SetupNotificationContextToAddNotFoundNotification()
        {
            _notificationContextMock
                .Setup(nc => nc.AddNotification("Ride not found"))
                .Verifiable();
        }

        private void VerifyNotificationContextNotCalled()
        {
            _notificationContextMock.Verify(nc => nc.AddNotification(It.IsAny<string>()), Times.Never);
        }

        private void VerifyNotificationContextCalledWithNotFound()
        {
            _notificationContextMock.Verify(nc => nc.AddNotification("Ride not found"), Times.Once);
        }
    }
}
