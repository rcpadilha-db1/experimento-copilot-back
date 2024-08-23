using Experimento.Data.Persistence;
using Experimento.Domain.Interfaces;

namespace Experimento.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ExperimentoContext _context;

    public UnitOfWork(ExperimentoContext context)
    {
        _context = context;
    }
    
    public async Task Commit(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}