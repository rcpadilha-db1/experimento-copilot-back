namespace TesteCopilot.Repository.Models;

public class Ride : BaseEntity
{
    public int VeiculoId { get; set; }
    public Vehicle Vehicle { get; set; }
    public int UsuarioId { get; set; }
    public User Rider { get; set; }
    public DateTime Date { get; set; }
}
