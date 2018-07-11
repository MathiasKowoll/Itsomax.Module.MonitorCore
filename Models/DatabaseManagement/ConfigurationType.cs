using System.Collections.Generic;
using Itsomax.Data.Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace Itsomax.Module.MonitorCore.Models.DatabaseManagement
{
    public class ConfigurationType : EntityBase
    {
       public ConfigurationType(long id)
        {
            Id = id;
        }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<VendorConfiguration> VendorConfiguration { get; set; } = new List<VendorConfiguration>();
        public IList<DatabaseSystem> DatabaseSystem { get; set; } = new List<DatabaseSystem>();
    }
}
