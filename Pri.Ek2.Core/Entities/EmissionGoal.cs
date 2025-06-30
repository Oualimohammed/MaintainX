using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ek2.Core.Entities
{
    public class EmissionGoal
    {
        public int Id { get; set; }
        public string UserId { get; set; } 
        public decimal TargetEmissionsKg { get; set; } 
        public DateTime StartDate { get; set; } // Start date of the goal
        public DateTime EndDate { get; set; } // End date of the goal
        public decimal CurrentEmissionsKg { get; set; }
        public bool IsAchieved { get; set; } 
        public UserProfile UserProfile { get; set; } 
        

    }
}
