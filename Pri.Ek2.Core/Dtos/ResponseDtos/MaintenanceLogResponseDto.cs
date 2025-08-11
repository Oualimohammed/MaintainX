using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ek2.Core.Dtos.ResponseDtos
{
    public class MaintenanceLogResponseDto
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public string Description { get; set; }
        public decimal? NewEmissionFactor { get; set; }
        public int? MileageAtMaintenance { get; set; }
        public List<string>? AttachmentPaths { get; set; }
        public string? Status { get; set; }
        public bool IsScheduled { get; set; }

    }
}
