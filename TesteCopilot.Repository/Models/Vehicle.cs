namespace TesteCopilot.Repository.Models;

public class Vehicle : BaseEntity
{

    public string Plate { get; set; }
    public int Capacity { get; set; }
    public int OwenerId { get; set; }
    public User Owener { get; set; }

}
