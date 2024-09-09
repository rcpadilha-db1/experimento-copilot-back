namespace experimento_copilot_back.Entities
{
    public class Ride
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        public Guid RiderId { get; set; }
        public User Rider { get; set; }

        public DateTime Date { get; set; }
    }
}
