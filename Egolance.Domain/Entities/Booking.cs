using System;
using System.Collections.Generic;
using System.Text;
using Egolance.Domain.Enums;

namespace Egolance.Domain.Entities
{

    public class Booking
    {
        public Guid BookingId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid WorkerId { get; set; }
        public Guid ServiceCategoryId { get; set; }

        public string Description { get; set; } = default!;
        public string Address { get; set; } = default!;
        public DateTime ScheduledDate { get; set; }
        public BookingStatus Status { get; set; }
        public decimal Price { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation
        public User Customer { get; set; } = default!;
        public Worker Worker { get; set; } = default!;
        public ServiceCategory ServiceCategory { get; set; } = default!;
        public Payment? Payment { get; set; }
        public Review? Review { get; set; }
        public ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();
    }

}
