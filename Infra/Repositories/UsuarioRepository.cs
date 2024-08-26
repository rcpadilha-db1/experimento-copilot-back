using Domain.Usuarios;
using Domain.Usuarios.Interfaces;
using Infra._Base;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
{
    private readonly ApiContext _context;

    public UsuarioRepository(ApiContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> UsuarioExisteAsync(string idUsuario)
        => await _context.Usuarios.AsQueryable().AnyAsync(u => u.Id == idUsuario);
}