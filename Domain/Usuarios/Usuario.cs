using Domain._Base.Models;
using Domain.Caronas;
using Domain.Veiculos;

namespace Domain.Usuarios;

public class Usuario : Entidade
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public virtual ICollection<Carona> Caronas { get; set; }
    public virtual ICollection<Veiculo> Veiculos { get; set; }
}