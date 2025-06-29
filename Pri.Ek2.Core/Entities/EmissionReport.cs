using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ek2.Core.Entities
{
    public class EmissionReport
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public decimal TotalEmissionsKg { get; set; } 
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        public string? Notes { get; set; } // Optional notes about the report
    }
}
