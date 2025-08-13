using System.ComponentModel.DataAnnotations;

namespace Pri.Ek2.Client.Dtos.RequestDtos
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
        public string Status { get; set; } = "Pending";

        public string? Notes { get; set; }
    }
}
