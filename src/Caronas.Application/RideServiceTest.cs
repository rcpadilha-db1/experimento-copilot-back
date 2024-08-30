using NUnit.Framework;
using Moq;
using Caronas.Application;
using Caronas.Persistence.Contratos;
using Caronas.Domain;

[TestFixture]
public class RideServiceTests
{
    private RideService _rideService;
    private Mock<IGeralPersist> _geralPersistMock;
    private Mock<IRidePersist> _ridePersistMock;

    [SetUp]
    public void Setup()
    {
        _geralPersistMock = new Mock<IGeralPersist>();
        _ridePersistMock = new Mock<IRidePersist>();
        _rideService = new RideService(_geralPersistMock.Object, _ridePersistMock.Object);
    }

    [Test]
    public async Task AddRide_ValidModel_ReturnsRide()
    {
        // Arrange
        var model = new Ride { /* initialize properties */ };
        _geralPersistMock.Setup(mock => mock.SaveChangesAsync()).ReturnsAsync(true);
        _ridePersistMock.Setup(mock => mock.GetRideByIdAsync(model.Id)).ReturnsAsync(model);

        // Act
        var result = await _rideService.AddRide(model);

        // Assert
        Assert.AreEqual(model, result);
    }

    [Test]
    public async Task UpdateRide_ExistingRide_ReturnsUpdatedRide()
    {
        // Arrange
        var rideId = "rideId";
        var model = new Ride { /* initialize properties */ };
        var existingRide = new Ride { /* initialize properties */ };
        _ridePersistMock.Setup(mock => mock.GetRideByIdAsync(rideId)).ReturnsAsync(existingRide);
        _geralPersistMock.Setup(mock => mock.SaveChangesAsync()).ReturnsAsync(true);
        _ridePersistMock.Setup(mock => mock.GetRideByIdAsync(model.Id)).ReturnsAsync(model);

        // Act
        var result = await _rideService.UpdateRide(rideId, model);

        // Assert
        Assert.AreEqual(model, result);
    }

    [Test]
    public async Task DeleteRide_ExistingRide_ReturnsTrue()
    {
        // Arrange
        var rideId = "rideId";
        var existingRide = new Ride { /* initialize properties */ };
        _ridePersistMock.Setup(mock => mock.GetRideByIdAsync(rideId)).ReturnsAsync(existingRide);
        _geralPersistMock.Setup(mock => mock.SaveChangesAsync()).ReturnsAsync(true);

        // Act
        var result = await _rideService.DeleteRide(rideId);

        // Assert
        Assert.IsTrue(result);
    }

    // Add more test methods for the remaining methods in RideService
}