using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ek2.Core.Dtos.RequestDtos
{
    public class MaintenanceLogRequestDto
    {
        [Required]
        public int VehicleId { get; set; }

        [Required]
        public DateTime MaintenanceDate { get; set; }

        [Required]
        public string Description { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? NewEmissionFactor { get; set; }
    }
}
