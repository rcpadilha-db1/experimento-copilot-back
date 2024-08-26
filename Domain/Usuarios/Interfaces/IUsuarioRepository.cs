using Domain._Base.Interfaces;

namespace Domain.Usuarios.Interfaces;

public  interface IUsuarioRepository : IRepositoryBase<Usuario>
{
    Task<bool> UsuarioExisteAsync(string idUsuario);
}