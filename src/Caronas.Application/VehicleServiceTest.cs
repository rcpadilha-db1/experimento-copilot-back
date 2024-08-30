using NUnit.Framework;
using Moq;
using System;
using System.Threading.Tasks;
using Caronas.Application;
using Caronas.Persistence.Contratos;
using Caronas.Domain;

[TestFixture]
public class VehicleServiceTests
{
    private VehicleService _vehicleService;
    private Mock<IGeralPersist> _geralPersistMock;
    private Mock<IVehiclePersist> _vehiclePersistMock;

    [SetUp]
    public void Setup()
    {
        _geralPersistMock = new Mock<IGeralPersist>();
        _vehiclePersistMock = new Mock<IVehiclePersist>();
        _vehicleService = new VehicleService(_geralPersistMock.Object, _vehiclePersistMock.Object);
    }

    [Test]
    public async Task AddVehicle_ValidModel_ReturnsVehicle()
    {
        // Arrange
        var model = new Vehicle { /* initialize properties */ };
        _geralPersistMock.Setup(mock => mock.Add<Vehicle>(model));
        _geralPersistMock.Setup(mock => mock.SaveChangesAsync()).ReturnsAsync(true);
        _vehiclePersistMock.Setup(mock => mock.GetVehicleByIdAsync(model.Id)).ReturnsAsync(model);

        // Act
        var result = await _vehicleService.AddVehicle(model);

        // Assert
        Assert.AreEqual(model, result);
    }

    // Add more test methods for the remaining methods in VehicleService
}