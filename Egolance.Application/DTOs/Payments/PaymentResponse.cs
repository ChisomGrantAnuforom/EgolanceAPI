using System;
using System.Collections.Generic;
using System.Text;

namespace Egolance.Application.DTOs.Payments
{
    public class PaymentResponse
    {
        public Guid PaymentId { get; set; }
        public Guid BookingId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid WorkerId { get; set; }

        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; }
        public string ClientSecret { get; set; }
    }

}
