namespace Experimento.Domain.Entities;

public class Vehicle
{
    public string Id { get; set; }
    public string Plate { get; set; }
    public int Capacity { get; set; }
    public string OwnerId { get; set; }
    public virtual User Owner { get; set; }
}