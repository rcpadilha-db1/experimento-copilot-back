using Crosscutting.Constantes;
using Crosscutting.Exceptions;
using Domain.Caronas.interfaces;

namespace Domain.Caronas.Services;

public class RemocaoCaronaService(ICaronaRepository caronaRepository) : IRemocaoCaronaService
{
    public async Task RemoverAsync(string idCarona)
    {
        var carona = await caronaRepository.ObterPorIdAsync(idCarona);
        if (carona is null)
            throw new NotFoundException(string.Format(MensagensErro.CaronaNaoEncontrada, idCarona));

        await caronaRepository.RemoverAsync(carona);
    }
}