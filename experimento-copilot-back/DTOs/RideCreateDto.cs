namespace experimento_copilot_back.DTOs
{
    public class RideCreateDto
    {
        public Guid VehicleId { get; set; }
        public Guid RiderId { get; set; }
        public DateTime Date { get; set; }
    }
}
