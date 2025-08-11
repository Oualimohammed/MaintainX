using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ek2.Core.Entities
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthData { get; set; }
        public string? ProfileImagePath { get; set; }

        public IdentityUser IdentityUser { get; set; } 
    }
}
