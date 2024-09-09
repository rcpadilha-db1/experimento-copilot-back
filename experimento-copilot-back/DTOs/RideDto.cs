namespace experimento_copilot_back.DTOs
{
    public class RideDto
    {
        public Guid VehicleId { get; set; }
        public VehicleDto Vehicle { get; set; }

        public Guid RiderId { get; set; }
        public UserDto Rider { get; set; }

        public DateTime Date { get; set; }
    }
}
