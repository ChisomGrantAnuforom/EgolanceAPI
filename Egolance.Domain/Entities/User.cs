using System;
using System.Collections.Generic;
using System.Text;
using Egolance.Domain.Enums;

namespace Egolance.Domain.Entities
{

    public class User
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public UserRole Role { get; set; }
        public string? ProfileImageUrl { get; set; }
        public bool IsVerified { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLogin { get; set; }

        // Navigation
        public Worker? WorkerProfile { get; set; }
        public ICollection<Booking> CustomerBookings { get; set; } = new List<Booking>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }


}
