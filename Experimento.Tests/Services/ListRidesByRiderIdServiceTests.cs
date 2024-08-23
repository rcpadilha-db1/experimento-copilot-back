using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Experimento.Application.Services;
using Experimento.Application.UseCases;
using Experimento.Domain.Entities;
using Experimento.Domain.Interfaces;
using Moq;
using Xunit;

namespace Experimento.Tests.Services
{
    public class ListRidesByRiderIdServiceTests
    {
        private readonly Mock<IRideRepository> _rideRepositoryMock;
        private readonly ListRidesByRiderIdService _service;

        public ListRidesByRiderIdServiceTests()
        {
            _rideRepositoryMock = new Mock<IRideRepository>();
            _service = new ListRidesByRiderIdService(_rideRepositoryMock.Object);
        }

        [Fact]
        public async Task ValidateRideAsync_ShouldReturnRideDetails_WhenRidesExist()
        {
            // Arrange
            var riderId = "rider123";
            var rides = CreateRidesMock();
            SetupRideRepository(riderId, rides);

            // Act
            var result = await _service.ValidateRideAsync(riderId, CancellationToken.None);

            // Assert
            AssertRides(result, rides);
        }

        [Fact]
        public async Task ValidateRideAsync_ShouldReturnEmptyList_WhenNoRidesExist()
        {
            // Arrange
            var riderId = "rider123";
            SetupRideRepository(riderId, new List<Ride>());

            // Act
            var result = await _service.ValidateRideAsync(riderId, CancellationToken.None);

            // Assert
            Assert.Empty(result);
        }

        private void SetupRideRepository(string riderId, List<Ride> rides)
        {
            _rideRepositoryMock
                .Setup(r => r.ListRidesByRiderId(riderId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(rides);
        }

        private static List<Ride> CreateRidesMock()
        {
            return new List<Ride>
            {
                new Ride
                {
                    Date = new DateTime(2024, 8, 26),
                    Vehicle = new Vehicle { Plate = "ABC123", Owner = new User { Name = "John Doe" } }
                },
                new Ride
                {
                    Date = new DateTime(2024, 8, 27),
                    Vehicle = new Vehicle { Plate = "XYZ789", Owner = new User { Name = "Jane Smith" } }
                }
            };
        }

        private static void AssertRides(IReadOnlyCollection<ListRidesByRiderIdResult> result, List<Ride> expectedRides)
        {
            Assert.NotEmpty(result);
            Assert.Equal(expectedRides.Count, result.Count);

            foreach (var ride in expectedRides)
            {
                Assert.Contains(result, rd => rd.Date == ride.Date &&
                                              rd.VehiclePlate == ride.Vehicle.Plate &&
                                              rd.VehicleOwnerName == ride.Vehicle.Owner.Name);
            }
        }
    }
}
