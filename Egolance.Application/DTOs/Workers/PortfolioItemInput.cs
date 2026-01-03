using System;
using System.Collections.Generic;
using System.Text;

namespace Egolance.Application.DTOs.Workers
{
    public class PortfolioItemInput
    {
        public string Title { get; set; }
        public string FileUrl { get; set; } // URL from Cloudinary or local storage
        public string FileType { get; set; } // image, certificate, video
    }

}
