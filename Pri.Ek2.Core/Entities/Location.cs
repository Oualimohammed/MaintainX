using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ek2.Core.Entities
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; } // Breedtegraad
        public double Longitude { get; set; } // Lengtegraad
        public string Address { get; set; }

        public ICollection<TransportRoute> StartRoutes { get; set; }
        public ICollection<TransportRoute> EndRoutes { get; set; }
    }
}
