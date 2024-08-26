namespace Domain.Caronas.interfaces;

public interface IRemocaoCaronaService
{
    Task RemoverAsync(string idCarona);
}