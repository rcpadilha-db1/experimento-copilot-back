using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SemCopilot.Models
{
    public class Vehicle
    {
        public string Id { get; set; }
        public string Plate { get; set; }
        public int Capacity { get; set; }

        public string OwnerId { get; set; }
        public User Owner { get; set; }

        public ICollection<Ride> Rides { get; set; }
    }
}