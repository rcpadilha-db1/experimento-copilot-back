using Caronas.Persistence.Contratos;
using Caronas.Presistence.Contextos;

namespace Caronas.Persistence
{
    public class GeralPersist : IGeralPersist
    {
        private readonly CaronasContext _context;
        public GeralPersist(CaronasContext context)
        {
            _context = context;
            
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void DeleteRange<T>(T[] entityArray) where T : class
        {
            _context.RemoveRange(entityArray);
        }
    }
}