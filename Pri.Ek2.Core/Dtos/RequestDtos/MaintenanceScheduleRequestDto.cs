using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ek2.Core.Dtos.RequestDtos
{
    public class MaintenanceScheduleRequestDto
    {
        [Required]
        public int VehicleId { get; set; }

        [Required]
        public DateTime LastMaintenanceDate { get; set; }

        [Required]
        public DateTime NextMaintenanceDueDate { get; set; }

        public int? MileageAtLastMaintenance { get; set; }

        public int? NextMaintenanceMileage { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Pending";

        [StringLength(500)]
        public string? Notes { get; set; }
    }
}
