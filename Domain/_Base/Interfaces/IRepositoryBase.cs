namespace Domain._Base.Interfaces;

public interface IRepositoryBase<TEntidade>
{
    Task<TEntidade?> ObterPorIdAsync(string id);
    Task AdicionarAsync(TEntidade obj);
    Task RemoverAsync(TEntidade obj);
}