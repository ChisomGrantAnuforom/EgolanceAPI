using System;
using System.Collections.Generic;
using System.Text;

namespace Egolance.Application.DTOs.Workers
{
    public class ReviewResponse
    {
        public Guid ReviewId { get; set; }
        public Guid CustomerId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
