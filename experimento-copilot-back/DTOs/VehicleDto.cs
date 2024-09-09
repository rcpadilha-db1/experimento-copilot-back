using experimento_copilot_back.Entities;

namespace experimento_copilot_back.DTOs
{
    public class VehicleDto
    {
        public string Plate { get; set; }
        public int Capacity { get; set; }

        public Guid OwnerId { get; set; }
        public User? Owner { get; set; }
    }
}
