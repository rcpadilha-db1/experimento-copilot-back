using Crosscutting.Constantes;
using Crosscutting.Exceptions;
using Crosscutting.Requests;
using Domain.Caronas.interfaces;
using Domain.Usuarios.Interfaces;
using Domain.Veiculos.Interfaces;

namespace Domain.Caronas.Services;

public class CadastroCaronaService(
    ICadastroCaronaValidator validator,
    ICaronaRepository repository,
    IUsuarioRepository usuarioRepository,
    IVeiculoRepository veiculoRepository
    ) : ICadastroCaronaService
{
    public async Task<string> CadastrarAsync(CaronaRequest request)
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid) 
        {
            var messageError = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage).ToList());
            throw new ValidateException(messageError);
        }

        await ValidarRequest(request);
        
        var carona = new Carona()
        {
            VeiculoId = request.VeiculoId,
            UsuarioId = request.UsuarioId,
            Data = request.Data
        };
        
        await repository.AdicionarAsync(carona);
        
        return carona.Id;
    }

    private async Task ValidarRequest(CaronaRequest request)
    {
        await ValidarUsuario(request.UsuarioId);
        await ValidarVeiculo(request.VeiculoId);
        await ValidarDisponibilidadeVeiculo(request);
        await ValidarCaronaMarcada(request);
    }
    
    private async Task ValidarCaronaMarcada(CaronaRequest request)
    {
        if (await repository.ExisteCaronaPorDiaEUsuarioAsync(request.Data, request.UsuarioId))
            throw new DomainException(string.Format(MensagensErro.UsuarioJaPossuiCaronaMarcada, request.UsuarioId));
    }

    private async Task ValidarDisponibilidadeVeiculo(CaronaRequest request)
    {
        var veiculo = await veiculoRepository.ObterPorIdComCaronasAsync(request.VeiculoId);
        var qtdeCaronasDia = veiculo.Caronas.Count(c => c.Data.Date == request.Data.Date);
        if (qtdeCaronasDia >= veiculo.Capacidade)
            throw new DomainException(string.Format(MensagensErro.VeiculoLotado, veiculo.Placa));
    }

    private async Task ValidarVeiculo(string veiculoId)
    {
        if (!await veiculoRepository.VeiculoExisteAsync(veiculoId))
            throw new NotFoundException(string.Format(MensagensErro.VeiculoNaoEncontrado, veiculoId));
    }

    private async Task ValidarUsuario(string usuarioId)
    {
        if (!await usuarioRepository.UsuarioExisteAsync(usuarioId))
            throw new NotFoundException(string.Format(MensagensErro.UsuarioNaoEncontrado, usuarioId));
    }
}