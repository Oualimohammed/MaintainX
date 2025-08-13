using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ek2.Client.Dtos.RequestDtos
{
    public class UserProfileRequestDto
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthData { get; set; }
        public string? ProfileImagePath { get; set; }
    }
}
