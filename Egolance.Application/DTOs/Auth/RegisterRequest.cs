using Egolance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Egolance.Application.DTOs.Auth
{
    public class RegisterRequest
    {
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Password { get; set; } = default!;
        public UserRole Role { get; set; }
    }

}
