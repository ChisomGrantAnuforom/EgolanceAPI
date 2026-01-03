using System;
using System.Collections.Generic;
using System.Text;

namespace Egolance.Application.DTOs.Booking
{
    public class CreateBookingRequest
    {
        public Guid WorkerId { get; set; }
        public Guid ServiceCategoryId { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public DateTime ScheduledDate { get; set; }
        public decimal Price { get; set; }
    }

}
