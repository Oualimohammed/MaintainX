using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ek2.Core.Dtos.ResponseDtos
{
    public class EmissionGoalResponseDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public decimal TargetEmissionsKg { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal CurrentEmissionsKg { get; set; }
        public bool IsAchieved { get; set; }
    }
}
