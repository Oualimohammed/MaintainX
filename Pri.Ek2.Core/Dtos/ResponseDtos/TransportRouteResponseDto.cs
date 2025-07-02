using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ek2.Core.Dtos.ResponseDtos
{
    public class TransportRouteResponseDto
    {
        public int Id { get; set; }
        public LocationResponseDto StartLocation { get; set; }
        public LocationResponseDto EndLocation { get; set; }
        public VehicleResponseDto Vehicle { get; set; }
        public decimal DistanceKm { get; set; }
        public decimal EstimatedEmissionsKg { get; set; }
        public string? ProofPath { get; set; }
    }
}
