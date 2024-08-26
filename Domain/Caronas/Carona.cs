using Domain._Base.Models;
using Domain.Usuarios;
using Domain.Veiculos;

namespace Domain.Caronas;

public class Carona : Entidade
{
    public string VeiculoId { get; set; }
    public string UsuarioId { get; set; }
    public DateTime Data { get; set; }
    
    public virtual Veiculo Veiculo { get; set; }
    
    public virtual Usuario Usuario { get; set; }
}