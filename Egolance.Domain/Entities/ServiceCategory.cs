using System;
using System.Collections.Generic;
using System.Text;

namespace Egolance.Domain.Entities
{
    public class ServiceCategory
    {
        public Guid ServiceCategoryId { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string? IconUrl { get; set; }

        // Navigation
        public ICollection<Worker> Workers { get; set; } = new List<Worker>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }

}
