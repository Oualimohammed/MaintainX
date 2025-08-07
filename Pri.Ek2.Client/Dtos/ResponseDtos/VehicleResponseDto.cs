using Pri.Ek2.Client.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ek2.Client.Dtos.ResponseDtos
{
    public class VehicleResponseDto
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public string Model { get; set; }
        public decimal EmissionFactor { get; set; }
        public VehicleType Type { get; set; }
        public int CurrentMileage { get; set; }
        public string Status { get; set; }
    }
}



