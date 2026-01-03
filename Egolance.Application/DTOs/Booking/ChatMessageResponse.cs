using System;
using System.Collections.Generic;
using System.Text;

namespace Egolance.Application.DTOs.Booking
{
    public class ChatMessageResponse
    {
        public Guid MessageId { get; set; }
        public Guid SenderId { get; set; }
        public string MessageText { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; }
    }

}
