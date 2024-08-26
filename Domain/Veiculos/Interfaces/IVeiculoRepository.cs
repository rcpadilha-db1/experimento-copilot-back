using Domain._Base.Interfaces;

namespace Domain.Veiculos.Interfaces;

public interface IVeiculoRepository : IRepositoryBase<Veiculos.Veiculo>
{
    Task<bool> VeiculoExisteAsync(string idVeiculo);
    Task<Veiculo> ObterPorIdComCaronasAsync(string idVeiculo);
}