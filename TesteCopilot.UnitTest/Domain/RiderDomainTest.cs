using Microsoft.EntityFrameworkCore;
using TesteCopilot.Domain.Riders;
using TesteCopilot.Repository.AppContext;
using TesteCopilot.Repository.Models;

public class RiderDomainTests
{
    private AppDatabaseContext GetInMemoryDbContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<AppDatabaseContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

        var context = new AppDatabaseContext(options);
        return context;
    }

    [Fact]
    public async Task ValidateRiderAsync_ReturnsFalse_WhenUserIsNull()
    {
        // Arrange
        var context = GetInMemoryDbContext("TestDatabase1");
        context.Vehicles.Add(new Vehicle { Id = 1, Plate = "ABC123", Capacity = 4 });
        await context.SaveChangesAsync();

        var riderDomain = new RiderDomain(context);

        // Act
        var result = await riderDomain.ValidateRiderAsync(1, 1);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task ValidateRiderAsync_ReturnsFalse_WhenVehicleIsNull()
    {
        // Arrange
        var context = GetInMemoryDbContext("TestDatabase2");
        await context.Users.AddAsync(new User { Id = 1, Name = "John Doe", Email = "john.doe@example.com" });

        var riderDomain = new RiderDomain(context);

        // Act
        var result = await riderDomain.ValidateRiderAsync(1, 1);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task ValidateRiderAsync_ReturnsFalse_WhenUserOrVehicleFieldsAreInvalid()
    {
        // Arrange
        var context = GetInMemoryDbContext("TestDatabase2");
        context.Users.Add(new User { Id = 1, Name = "", Email = "john.doe@example.com" });
        context.Vehicles.Add(new Vehicle { Id = 1, Plate = "ABC123", Capacity = 4 });
        await context.SaveChangesAsync();

        var riderDomain = new RiderDomain(context);

        // Act
        var result = await riderDomain.ValidateRiderAsync(1, 1);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task ValidateRiderAsync_ReturnsTrue_WhenUserAndVehicleAreValid()
    {
        // Arrange
        var context = GetInMemoryDbContext("TestDatabase3");
        await context.Users.AddAsync(new User { Id = 1, Name = "John Doe", Email = "john.doe@example.com" });
        await context.Vehicles.AddAsync(new Vehicle { Id = 1, Plate = "ABC123", Capacity = 4, OwenerId = 1 });

        var riderDomain = new RiderDomain(context);

        // Act
        var result = await riderDomain.ValidateRiderAsync(1, 1);

        // Assert
        Assert.True(result);
    }
}