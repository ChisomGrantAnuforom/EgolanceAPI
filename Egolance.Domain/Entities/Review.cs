using System;
using System.Collections.Generic;
using System.Text;

namespace Egolance.Domain.Entities
{
    public class Review
    {
        public Guid ReviewId { get; set; }
        public Guid BookingId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid WorkerId { get; set; }

        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation
        public Booking Booking { get; set; } = default!;
        public User Customer { get; set; } = default!;
        public Worker Worker { get; set; } = default!;
    }

}
