namespace experimento_copilot_back.Entities
{
    public class Vehicle
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Plate { get; set; }
        public int Capacity { get; set; }

        public Guid OwnerId { get; set; }
        public User Owner { get; set; }
    }
}
