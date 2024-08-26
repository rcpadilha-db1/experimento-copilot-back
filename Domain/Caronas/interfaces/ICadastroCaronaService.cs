using Crosscutting.Requests;

namespace Domain.Caronas.interfaces;

public interface ICadastroCaronaService
{
    Task<string> CadastrarAsync(CaronaRequest request);
}