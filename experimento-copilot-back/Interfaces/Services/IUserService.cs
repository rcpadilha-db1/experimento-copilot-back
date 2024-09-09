using experimento_copilot_back.Entities;

namespace experimento_copilot_back.Interfaces.Services
{
    public interface IUserService
    {
        Task CreateUserAsync(User user);
        Task<IList<User>> GetAllUsersAsync();
    }

}
