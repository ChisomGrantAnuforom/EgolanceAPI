using System;
using System.Collections.Generic;
using System.Text;

namespace Egolance.Domain.Entities
{
    public class PortfolioItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid WorkerId { get; set; }
        public string Title { get; set; }
        public string FileUrl { get; set; } // URL to image/certificate
        public string FileType { get; set; } // "image", "certificate", "video"
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Worker Worker { get; set; }
    }

}
