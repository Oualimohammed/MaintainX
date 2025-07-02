using Pri.Ek2.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ek2.Core.Dtos.RequestDtos
{
    public class VehicleRequestDto
    {
        [Required]
        public string LicensePlate { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public VehicleType Type { get; set; }

        [Range(0, double.MaxValue)]
        public decimal EmissionFactor { get; set; }
    }
}
