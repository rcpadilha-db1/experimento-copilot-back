using Crosscutting.Responses;

namespace Domain.Caronas.interfaces;

public interface IListagemCaronaService
{
    Task<List<CaronasPorUsuarioResponse>> ListarCaronasAsync(string usuarioId);
}