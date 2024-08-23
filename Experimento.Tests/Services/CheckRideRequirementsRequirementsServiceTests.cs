using System.Threading;
using System.Threading.Tasks;
using Experimento.Application.Services;
using Experimento.Domain.Interfaces;
using Experimento.Domain.Notification;
using Moq;
using Xunit;

namespace Experimento.Tests.Services
{
    public class CheckRideRequirementsRequirementsServiceTests
    {
        private readonly Mock<IRideRepository> _rideRepositoryMock;
        private readonly Mock<NotificationContext> _notificationContextMock;
        private readonly CheckRideRequirementsRequirementsService _service;

        public CheckRideRequirementsRequirementsServiceTests()
        {
            _rideRepositoryMock = new Mock<IRideRepository>();
            _notificationContextMock = new Mock<NotificationContext>();

            _service = new CheckRideRequirementsRequirementsService(
                _rideRepositoryMock.Object,
                _notificationContextMock.Object
            );
        }

        [Fact]
        public async Task CheckIfAreRequirementsToRide_ShouldAddNotification_WhenRequirementsAreNotMet()
        {
            // Arrange
            var rideId = "ride9";
            var riderId = "userId";
            var vehicleId = "vehicleId";
            SetupRideRepositoryToReturnInvalid(rideId, riderId, vehicleId);

            // Act
            await _service.CheckIfAreRequirementsToRide(rideId, riderId, vehicleId, CancellationToken.None);

            // Assert
            _notificationContextMock.Verify(nc => nc.AddNotification("Invalid Ride requirements"), Times.Once);
        }

        [Fact]
        public async Task CheckIfAreRequirementsToRide_ShouldNotAddNotification_WhenRequirementsAreMet()
        {
            // Arrange
            var rideId = "ride9";
            var riderId = "userId";
            var vehicleId = "vehicleId";
            SetupRideRepositoryToReturnValid(rideId, riderId, vehicleId);

            // Act
            await _service.CheckIfAreRequirementsToRide(rideId, riderId, vehicleId, CancellationToken.None);

            // Assert
            _notificationContextMock.Verify(nc => nc.AddNotification(It.IsAny<string>()), Times.Never);
        }

        private void SetupRideRepositoryToReturnInvalid(string rideId, string riderId, string vehicleId)
        {
            _rideRepositoryMock
                .Setup(r => r.AreRideExistsAsync(rideId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);
            _rideRepositoryMock
                .Setup(r => r.AreUserExistsAsync(riderId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);
            _rideRepositoryMock
                .Setup(r => r.AreVehicleExistsAsync(vehicleId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);
        }

        private void SetupRideRepositoryToReturnValid(string rideId, string riderId, string vehicleId)
        {
            _rideRepositoryMock
                .Setup(r => r.AreRideExistsAsync(rideId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);
            _rideRepositoryMock
                .Setup(r => r.AreUserExistsAsync(riderId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);
            _rideRepositoryMock
                .Setup(r => r.AreVehicleExistsAsync(vehicleId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);
        }
    }
}
