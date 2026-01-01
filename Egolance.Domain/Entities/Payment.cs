using System;
using System.Collections.Generic;
using System.Text;
using Egolance.Domain.Enums;

namespace Egolance.Domain.Entities
{
    public class Payment
    {
        public Guid PaymentId { get; set; }
        public Guid BookingId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid WorkerId { get; set; }

        public decimal Amount { get; set; }
        public string Currency { get; set; } = "EUR";
        public string StripePaymentIntentId { get; set; } = default!;
        public PaymentStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation
        public Booking Booking { get; set; } = default!;
        public User Customer { get; set; } = default!;
        public Worker Worker { get; set; } = default!;
    }

}
