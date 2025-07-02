using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ek2.Core.Dtos.RequestDtos
{
    public class LocationsRequestDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Range(-90,90)]
        public double Latitude { get; set; }

        [Range(-180,180)]
        public double Longitude { get; set; }
    }
}
