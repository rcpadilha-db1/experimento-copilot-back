namespace Caronas.Domain
{
    public class Ride
    {
        public string? Id { get; set; }
        public string? VehicleId { get; set; }
        public Vehicle? Vehicle { get; set; }
        public string? UserId { get; set; }
        public User? User { get; set; }
        public DateTime Date { get; set; }
    }
}