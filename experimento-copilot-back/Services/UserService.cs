using experimento_copilot_back.Entities;
using experimento_copilot_back.Interfaces.Repositories;
using experimento_copilot_back.Interfaces.Services;

namespace experimento_copilot_back.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task CreateUserAsync(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Name) || string.IsNullOrWhiteSpace(user.Email))
                throw new ArgumentException("Dado(s) da requisição inválido(s).");

            await _userRepository.AddUserAsync(user);
        }

        public async Task<IList<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }
    }

}
