using System.ComponentModel.DataAnnotations.Schema;

namespace Pri.Ek2.Core.Entities
{
    public class TransportRoute
    {
        public int Id { get; set; }

        public int StartLocationId { get; set; }
        public int EndLocationId { get; set; }
        public int VehicleId { get; set; }

        [ForeignKey("StartLocationId")]
        public Location StartLocation { get; set; }

        [ForeignKey("EndLocationId")]
        public Location EndLocation { get; set; }

        [ForeignKey("VehicleId")]
        public Vehicle Vehicle { get; set; }

        public decimal DistanceKm { get; set; }
        public decimal EstimatedEmissionsKg { get; set; }
        public string? ProofPath { get; set; }
    }
}