using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ek2.Core.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public string Model { get; set; }
        public VehicleType Type { get; set; }
        public decimal EmissionFactor { get; set; } // hoeveelheid van kg CO2 per km
        public ICollection<TransportRoute> TransportRoutes { get; set; }
        public ICollection<MaintenanceLog> MaintenanceLogs { get; set; }
    }
}
