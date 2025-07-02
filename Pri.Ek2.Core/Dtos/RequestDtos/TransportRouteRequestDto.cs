using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ek2.Core.Dtos.RequestDtos
{
    public class TransportRouteRequestDto
    {
        [Required]
        public int StartLocationId { get; set; }

        [Required]
        public int EndLocationId { get; set; }

        [Required]
        public int VehicleId { get; set; }

        [Range(0.1, double.MaxValue)]
        public double DistanceKm { get; set; }

        public string? ProofPath { get; set; }
    }
}
