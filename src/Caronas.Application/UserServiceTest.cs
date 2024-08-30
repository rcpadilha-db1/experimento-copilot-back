using NUnit.Framework;
using Moq;
using System.Threading.Tasks;
using Caronas.Domain;
using Caronas.Application;
using Caronas.Persistence.Contratos;

[TestFixture]
public class UserServiceTests
{
    private UserService _userService;
    private Mock<IGeralPersist> _geralPersistMock;
    private Mock<IUserPersist> _userPersistMock;

    [SetUp]
    public void Setup()
    {
        _geralPersistMock = new Mock<IGeralPersist>();
        _userPersistMock = new Mock<IUserPersist>();
        _userService = new UserService(_geralPersistMock.Object, _userPersistMock.Object);
    }

    [Test]
    public async Task AddUser_ValidModel_ReturnsUser()
    {
        // Arrange
        var model = new User { /* initialize properties */ };
        _geralPersistMock.Setup(mock => mock.Add<User>(model));
        _geralPersistMock.Setup(mock => mock.SaveChangesAsync()).ReturnsAsync(true);
        _userPersistMock.Setup(mock => mock.GetUserByIdAsync(model.Id)).ReturnsAsync(model);

        // Act
        var result = await _userService.AddUser(model);

        // Assert
        Assert.AreEqual(model, result);
    }

    [Test]
    public async Task UpdateUser_ExistingUser_ReturnsUpdatedUser()
    {
        // Arrange
        var userId = "userId";
        var model = new User { /* initialize properties */ };
        var existingUser = new User { /* initialize properties */ };
        _userPersistMock.Setup(mock => mock.GetUserByIdAsync(userId)).ReturnsAsync(existingUser);
        _geralPersistMock.Setup(mock => mock.Update(model));
        _geralPersistMock.Setup(mock => mock.SaveChangesAsync()).ReturnsAsync(true);
        _userPersistMock.Setup(mock => mock.GetUserByIdAsync(model.Id)).ReturnsAsync(model);

        // Act
        var result = await _userService.UpdateUser(userId, model);

        // Assert
        Assert.AreEqual(model, result);
    }

    [Test]
    public async Task DeleteUser_ExistingUser_ReturnsTrue()
    {
        // Arrange
        var userId = "userId";
        var existingUser = new User { /* initialize properties */ };
        _userPersistMock.Setup(mock => mock.GetUserByIdAsync(userId)).ReturnsAsync(existingUser);
        _geralPersistMock.Setup(mock => mock.Delete<User>(existingUser));
        _geralPersistMock.Setup(mock => mock.SaveChangesAsync()).ReturnsAsync(true);

        // Act
        var result = await _userService.DeleteUser(userId);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public async Task GetAllUsersAsync_ReturnsUsersArray()
    {
        // Arrange
        var users = new User[] { /* initialize users */ };
        _userPersistMock.Setup(mock => mock.GetAllUsersAsync()).ReturnsAsync(users);

        // Act
        var result = await _userService.GetAllUsersAsync();

        // Assert
        Assert.AreEqual(users, result);
    }

    [Test]
    public async Task GetUserByIdAsync_ReturnsUser()
    {
        // Arrange
        var userId = "userId";
        var user = new User { /* initialize properties */ };
        _userPersistMock.Setup(mock => mock.GetUserByIdAsync(userId)).ReturnsAsync(user);

        // Act
        var result = await _userService.GetUserByIdAsync(userId);

        // Assert
        Assert.AreEqual(user, result);
    }

    // Add more test methods for the remaining methods in UserService
}