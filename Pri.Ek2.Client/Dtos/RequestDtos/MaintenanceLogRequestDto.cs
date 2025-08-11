using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ek2.Client.Dtos.RequestDtos
{
    public class MaintenanceLogRequestDto
    {
        [Required]
        public int VehicleId { get; set; }

        [Required]
        public DateTime MaintenanceDate { get; set; }

        [Required]
        public string Description { get; set; }

        public decimal? NewEmissionFactor { get; set; }
        public string Status { get; set; }
        public bool IsScheduled { get; set; }
        public int? MileageAtMaintenance { get; set; }

        public List<string>? AttachmentPaths { get; set; }
    }
}
