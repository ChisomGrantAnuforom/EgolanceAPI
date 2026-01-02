using System;
using System.Collections.Generic;
using System.Text;

namespace Egolance.Application.DTOs.Workers
{
    public class CreateWorkerRequest
    {
        public string Bio { get; set; } = default!;
        public decimal HourlyRate { get; set; }
        public Guid ServiceCategoryId { get; set; }
        public int ExperienceYears { get; set; }
        public double? LocationLat { get; set; }
        public double? LocationLng { get; set; }
    }

}
