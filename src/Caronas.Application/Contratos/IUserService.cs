using Caronas.Domain;

namespace Caronas.Application.Contratos
{
    public interface IUserService
    {
        Task<User> AddUser(User model);
        Task<User> UpdateUser(string userId, User model);
        Task<bool> DeleteUser(string userId);
        
        Task<User[]> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(string id);
    }
}