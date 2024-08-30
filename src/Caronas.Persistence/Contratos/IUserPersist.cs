using Caronas.Domain;

namespace Caronas.Persistence.Contratos
{
    public interface IUserPersist
    {
        Task<User[]> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(string id);
    }
}