using Domain.Veiculos;
using Domain.Veiculos.Interfaces;
using Infra._Base;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class VeiculoRepository  : RepositoryBase<Veiculo>, IVeiculoRepository
{
    private readonly ApiContext _context;

    public VeiculoRepository(ApiContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> VeiculoExisteAsync(string idVeiculo)
        => await _context.Veiculos.AsQueryable().AnyAsync(v => v.Id == idVeiculo);

    public async Task<Veiculo> ObterPorIdComCaronasAsync(string idVeiculo)
        => await _context.Veiculos.AsQueryable()
            .Include(v => v.Caronas)
            .Where(v => v.Id == idVeiculo)
            .FirstAsync();
}