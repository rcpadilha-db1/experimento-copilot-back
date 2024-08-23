using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SemCopilot.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
        public ICollection<Ride> Rides { get; set; }
    }
}