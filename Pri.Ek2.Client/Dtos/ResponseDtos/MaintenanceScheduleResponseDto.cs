namespace Pri.Ek2.Client.Dtos.ResponseDtos
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
