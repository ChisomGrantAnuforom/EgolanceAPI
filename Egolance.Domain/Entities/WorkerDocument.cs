using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Egolance.Domain.Enums;

namespace Egolance.Domain.Entities
{
    public class WorkerDocument
    {
        public Guid DocumentId { get; set; }
        public Guid WorkerId { get; set; }

        public DocumentType DocumentType { get; set; }
        public string DocumentUrl { get; set; } = default!;
        public VerificationStatus Status { get; set; }
        public DateTime UploadedAt { get; set; }

        // Navigation
        public Worker Worker { get; set; } = default!;
    }

}
