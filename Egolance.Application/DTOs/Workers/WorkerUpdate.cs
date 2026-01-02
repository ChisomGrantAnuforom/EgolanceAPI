using System;
using System.Collections.Generic;
using System.Text;

namespace Egolance.Application.DTOs.Workers
{


    public class WorkerUpdate
    {
        public string Bio { get; set; }
        public decimal HourlyRate { get; set; }
        public Guid ServiceCategoryId { get; set; }
        public int ExperienceYears { get; set; }
        public bool IsAvailable { get; set; }
        public double LocationLat { get; set; }
        public double LocationLng { get; set; }
    }


}
