using System;
using System.Collections.Generic;
using System.Text;

namespace Egolance.Domain.Entities
{
    public class ChatMessage
    {
        public Guid MessageId { get; set; }
        public Guid BookingId { get; set; }
        public Guid SenderId { get; set; }

        public string MessageText { get; set; } = default!;
        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; }

        // Navigation
        public Booking Booking { get; set; } = default!;
        public User Sender { get; set; } = default!;
    }

}
