using Pri.Ek2.Client.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ek2.Client.Dtos.RequestDtos
{
    public class VehicleRequestDto
    {
        public string LicensePlate { get; set; }
        public string Model { get; set; }
        public VehicleType Type { get; set; }
        public decimal EmissionFactor { get; set; }
    }
}
