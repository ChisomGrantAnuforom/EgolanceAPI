using System;
using System.Collections.Generic;
using System.Text;

namespace Egolance.Application.DTOs.Booking
{
    public class BookingResponse
    {
        public Guid BookingId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid WorkerId { get; set; }
        public Guid ServiceCategoryId { get; set; }

        public string Description { get; set; }
        public string Address { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string Status { get; set; }
        public decimal Price { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

}
