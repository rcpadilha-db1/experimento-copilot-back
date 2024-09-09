using experimento_copilot_back.Entities;

namespace experimento_copilot_back.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user);
        Task<IList<User>> GetAllUsersAsync();
    }

}
