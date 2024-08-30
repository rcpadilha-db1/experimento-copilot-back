namespace Caronas.Domain
{
    public class Vehicle
    {
        public string? Id { get; set; }
        public string? Plate { get; set; }
        public int Capacity { get; set; }
        public string? UserId { get; set; }
        public User? User { get; set; }
    }
}