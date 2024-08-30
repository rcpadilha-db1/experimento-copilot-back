using Caronas.Domain;
using Caronas.Persistence.Contratos;
using Caronas.Presistence.Contextos;
using Microsoft.EntityFrameworkCore;

namespace Caronas.Persistence
{
    public class UserPersist : IUserPersist
    {
        private readonly CaronasContext _context;
        public UserPersist(CaronasContext context)
        {
            _context = context;
        }

        public async Task<User[]> GetAllUsersAsync()
        {
            IQueryable<User> query = _context.Users;
                
            query = query.AsNoTracking().OrderBy(u => u.Id);

            return await query.ToArrayAsync();
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            IQueryable<User> query = _context.Users;

            query = query.AsNoTracking().OrderBy(u => u.Id)
                         .Where(u => u.Id == id);

            return await query.FirstOrDefaultAsync();
        }
    }
}