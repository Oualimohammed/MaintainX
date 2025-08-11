using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pri.Ek2.Core.Entities
{
    public class MaintenanceLog
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public string Description { get; set; }
        public decimal? NewEmissionFactor { get; set; }
        public Vehicle Vehicle { get; set; } = null!;
        public int? MileageAtMaintenance { get; set; }

        public string? AttachmentPathsJson { get; set; }

        [NotMapped]
        public List<string> AttachmentPaths
        {
            get => string.IsNullOrEmpty(AttachmentPathsJson)
                    ? new List<string>()
                    : JsonSerializer.Deserialize<List<string>>(AttachmentPathsJson)!;
            set => AttachmentPathsJson = JsonSerializer.Serialize(value);
        }
    }
}
