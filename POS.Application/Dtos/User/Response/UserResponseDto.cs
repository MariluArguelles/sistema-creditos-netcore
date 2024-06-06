using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Dtos.User.Response
{
    public class UserResponseDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Image { get; set; } = null!;

        public string? AuthType { get; set; }

        public string AuditCreateDate { get; set; }
        public int State { get; set; }
        
        public string? StateUser { get; set; }


    }
}
