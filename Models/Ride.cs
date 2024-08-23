using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SemCopilot.Models
{

    public class Ride
    {
        public string Id { get; set; }

        public string VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        public string RiderId { get; set; }
        public User Rider { get; set; }

        public DateTime Date { get; set; }
    }
}