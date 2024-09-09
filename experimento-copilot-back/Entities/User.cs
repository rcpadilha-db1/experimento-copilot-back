namespace experimento_copilot_back.Entities
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }
        public string Email { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
