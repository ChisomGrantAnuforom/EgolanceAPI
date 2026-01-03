using System;
using System.Collections.Generic;
using System.Text;

namespace Egolance.Application.DTOs.Workers
{
    public class PortfolioItemResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string FileUrl { get; set; }
        public string FileType { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
