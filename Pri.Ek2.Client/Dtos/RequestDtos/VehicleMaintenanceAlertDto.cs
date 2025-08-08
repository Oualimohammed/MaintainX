namespace Pri.Ek2.Client.Dtos.RequestDtos
{
    public class VehicleMaintenanceAlertDto
    {
        public string LicensePlate { get; set; }
        public DateTime? NextMaintenanceDue { get; set; }
    }
}
