using System;
using System.Collections.Generic;
using System.Text;

namespace Egolance.Application.DTOs.ServiceCategories
{
    public class ServiceCategoryInput
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
    }
}
