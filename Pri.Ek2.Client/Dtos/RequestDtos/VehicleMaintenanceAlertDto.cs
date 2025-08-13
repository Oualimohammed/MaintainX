namespace Pri.Ek2.Client.Dtos.RequestDtos
{
    public class VehicleMaintenanceAlertDto
    {
        public string LicensePlate { get; set; }
        public string VehicleModel { get; set; }
        public DateTime NextMaintenanceDue { get; set; }
        public int DaysUntilDue => (NextMaintenanceDue - DateTime.Today).Days;
        public string Status { get; set; }
    }
}
