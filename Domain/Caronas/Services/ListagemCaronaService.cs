using Crosscutting.Responses;
using Domain.Caronas.interfaces;

namespace Domain.Caronas.Services;

public class ListagemCaronaService(ICaronaRepository caronaRepository) : IListagemCaronaService
{
    public async Task<List<CaronasPorUsuarioResponse>> ListarCaronasAsync(string usuarioId)
    {
        var caronas = await caronaRepository.ListarCaronasPorUsuarioAsync(usuarioId);
        if (caronas.Count == 0)
            return new List<CaronasPorUsuarioResponse>();

        return caronas
            .Select(c =>
                new CaronasPorUsuarioResponse()
                {
                    Placa = c.Veiculo.Placa,
                    Nome = c.Veiculo.Usuario.Nome,
                    Data = c.Data
                }
            )
            .ToList();
    }
}