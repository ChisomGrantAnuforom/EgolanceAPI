using System;
using System.Collections.Generic;
using System.Text;
using Egolance.Domain.Enums;

namespace Egolance.Domain.Entities
{
    public class Notification
    {
        public Guid NotificationId { get; set; }
        public Guid UserId { get; set; }

        public string Title { get; set; } = default!;
        public string Message { get; set; } = default!;
        public NotificationType Type { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation
        public User User { get; set; } = default!;
    }

}
