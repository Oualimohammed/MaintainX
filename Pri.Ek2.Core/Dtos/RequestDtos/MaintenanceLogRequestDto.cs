using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ek2.Core.Dtos.RequestDtos
{
    public class MaintenanceLogRequestDto
    {
        [Required(ErrorMessage = "Voertuig ID is verplicht")]
        public int VehicleId { get; set; }

        [Required(ErrorMessage = "Onderhoudsdatum is verplicht")]
        public DateTime MaintenanceDate { get; set; }

        [Required(ErrorMessage = "Beschrijving is verplicht")]
        [MaxLength(255, ErrorMessage = "Beschrijving mag maximaal 255 tekens bevatten")]
        public string Description { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Emissiefactor moet positief zijn")]
        public decimal? NewEmissionFactor { get; set; }

        public int? MileageAtMaintenance { get; set; }

        public List<string>? AttachmentPaths { get; set; }

        public string? Status { get; set; }

        public bool IsScheduled { get; set; }
    }
}
