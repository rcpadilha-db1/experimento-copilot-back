using Domain._Base.Interfaces;

namespace Domain.Caronas.interfaces;

public interface ICaronaRepository : IRepositoryBase<Carona>
{
    Task<bool> ExisteCaronaPorDiaEUsuarioAsync(DateTime dataCarona, string usuarioId);
    Task<List<Carona>> ListarCaronasPorUsuarioAsync(string usuarioId);
}