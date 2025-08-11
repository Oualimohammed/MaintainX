using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ek2.Core.Dtos.RequestDtos
{
    public class ResetPasswordRequestDto
    {
        public string UserId { get; set; }
        public string NewPassword { get; set; }
    }
}

