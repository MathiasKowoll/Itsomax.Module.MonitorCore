using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Itsomax.Data.Infrastructure.Models;

namespace Itsomax.Module.MonitorCore.Models.DatabaseManagement
{
    public class Vendor : EntityBase
    {
        public Vendor(){}

        public Vendor(long id)
        {
            Id = id;
        }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [MaxLength(500)]
        public string Driver { get; set; }
        public IList<VendorConfiguration> VendorConfiguration { get; set; } = new List<VendorConfiguration>();
        public IList<DatabaseSystem> DatabaseSystem { get; set; } = new List<DatabaseSystem>();
    }
}
