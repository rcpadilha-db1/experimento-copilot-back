namespace Experimento.Domain.Entities;

public class Ride
{
    public string Id { get; set; }
    public string VehicleId { get; set; }
    public virtual Vehicle Vehicle { get; set; }
    public string RiderId { get; set; }
    public virtual User Rider { get; set; }
    public DateTime Date { get; set; }
}