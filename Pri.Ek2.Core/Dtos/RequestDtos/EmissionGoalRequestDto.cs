using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ek2.Core.Dtos.RequestDtos
{
    public class EmissionGoalRequestDto
    {
        [Required]
        public string UserId { get; set; }
        [Range (0, double.MaxValue)]
        public decimal TargetEmissionKg { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
    }
}
