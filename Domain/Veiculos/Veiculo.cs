using Domain._Base.Models;
using Domain.Caronas;
using Domain.Usuarios;

namespace Domain.Veiculos;

public class Veiculo : Entidade
{
    public string Placa { get; set; }
    public int Capacidade { get; set; }
    public string UsuarioId { get; set; }
    public virtual Usuario Usuario { get; set; }
    public virtual ICollection<Carona> Caronas { get; set; }
}