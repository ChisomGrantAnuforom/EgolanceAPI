using System;
using System.Collections.Generic;
using System.Text;
using Egolance.Domain.Enums;

namespace Egolance.Domain.Entities
{
    public class Worker
    {
        public Guid WorkerId { get; set; }   // FK to User
        public string Bio { get; set; } = default!;
        public decimal HourlyRate { get; set; }
        public Guid ServiceCategoryId { get; set; }
        public int ExperienceYears { get; set; }
        public double Rating { get; set; }
        public bool IsAvailable { get; set; }
        public double? LocationLat { get; set; }
        public double? LocationLng { get; set; }
        public VerificationStatus VerificationStatus { get; set; }

        // Navigation
        public User User { get; set; } = default!;
        public ServiceCategory ServiceCategory { get; set; } = default!;
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<WorkerDocument> Documents { get; set; } = new List<WorkerDocument>();

        public List<PortfolioItem> Portfolio { get; set; } = new();

        public List<Review> Reviews { get; set; } = new();


    }

}
