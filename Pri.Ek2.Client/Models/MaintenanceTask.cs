namespace Pri.Ek2.Client.Models
{
    public class MaintenanceTask
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public string VehicleLicense { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public bool IsOverdue { get; set; }
    }
}
