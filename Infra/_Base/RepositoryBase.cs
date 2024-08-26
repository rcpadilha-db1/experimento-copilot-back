using Domain._Base.Interfaces;
using Domain._Base.Models;
using Microsoft.EntityFrameworkCore;

namespace Infra._Base;

public abstract class RepositoryBase<TEntidade> : IRepositoryBase<TEntidade>
    where TEntidade : Entidade
{
    private readonly DbSet<TEntidade> _dbSet;
    private readonly ApiContext _context;

    protected RepositoryBase(ApiContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntidade>();
    }

    public async Task<TEntidade?> ObterPorIdAsync(string id) => await _dbSet.FindAsync(id);

    public async Task AdicionarAsync(TEntidade obj)
    {
        _dbSet.Add(obj);
        await _context.SaveChangesAsync();
    }

    public async Task RemoverAsync(TEntidade obj)
    {
        _dbSet.Remove(obj);
        await _context.SaveChangesAsync();
    }
}