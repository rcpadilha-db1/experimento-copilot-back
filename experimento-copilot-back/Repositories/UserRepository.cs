using experimento_copilot_back.Entities;
using experimento_copilot_back.Infra;
using experimento_copilot_back.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace experimento_copilot_back.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }

}
