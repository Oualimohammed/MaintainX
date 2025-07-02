using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ek2.Core.Dtos.RequestDtos
{
    public class EmissionReportRequestDto
    {
        [Required]
        public string UserId { get; set; }

        [Range(0, double.MaxValue)]
        public decimal TotalEmissionsKg { get; set; }

        [Required]
        public DateTime PeriodStart { get; set; }

        [Required]
        public DateTime PeriodEnd { get; set; }

        public string? Notes { get; set; }
    }
}
