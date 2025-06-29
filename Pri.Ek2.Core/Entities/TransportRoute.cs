using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ek2.Core.Entities
{
    public class TransportRoute
    {
        public int Id { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public decimal DistanceKm { get; set; }
        public decimal EstimatedEmissionsKg { get; set; }
        [ForeignKey("VehicleId")]
        public Vehicle Vehicle { get; set; } = null!; 
        public int VehicleId { get; set; }
        public string? ProofPath { get; set; }

    }
}
