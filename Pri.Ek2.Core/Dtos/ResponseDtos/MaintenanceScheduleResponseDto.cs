using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ek2.Core.Dtos.ResponseDtos
{
    public class MaintenanceScheduleResponseDto
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public DateTime LastMaintenanceDate { get; set; }
        public DateTime NextMaintenanceDueDate { get; set; }
        public int? MileageAtLastMaintenance { get; set; }
        public int? NextMaintenanceMileage { get; set; }
        public string Status { get; set; } = "Pending";
        public string? Notes { get; set; }

        public string? AssignedMechanicId { get; set; }
    }
}
