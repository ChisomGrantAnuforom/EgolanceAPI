using System;
using System.Collections.Generic;
using System.Text;

namespace Egolance.Application.DTOs.Workers
{
    public class CreateReviewRequest
    {
        public int Rating { get; set; } // 1–5
        public string Comment { get; set; }
    }

}
