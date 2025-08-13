using Pri.Ek2.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ek2.Core.Dtos.ResponseDtos
{
    public class VehicleResponseDto
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public string Model { get; set; }
        public VehicleType Type { get; set; }
        public decimal EmissionFactor { get; set; }
    }
}



