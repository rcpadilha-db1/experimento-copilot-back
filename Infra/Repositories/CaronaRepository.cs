using Domain.Caronas;
using Domain.Caronas.interfaces;
using Infra._Base;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class CaronaRepository : RepositoryBase<Carona>, ICaronaRepository
{
    private readonly ApiContext _context;
    
    public CaronaRepository(ApiContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task<bool> ExisteCaronaPorDiaEUsuarioAsync(DateTime dataCarona, string usuarioId)
        => await _context.Caronas.AsQueryable()
            .AnyAsync(c => c.Data.Date == dataCarona.Date && c.UsuarioId == usuarioId);

    public async Task<List<Carona>> ListarCaronasPorUsuarioAsync(string usuarioId)
        => await _context.Caronas.AsQueryable()
            .Include(c => c.Veiculo)
            .ThenInclude(v => v.Usuario)
            .Where(c => c.UsuarioId == usuarioId)
            .ToListAsync();
}