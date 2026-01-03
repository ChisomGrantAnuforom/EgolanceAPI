using System;
using System.Collections.Generic;
using System.Text;

namespace Egolance.Application.DTOs.Notifications
{
    public class NotificationMessage
    {
        public Guid UserId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }

}
