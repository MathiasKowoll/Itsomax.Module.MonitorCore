using System.Collections.Generic;
using Itsomax.Data.Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace Itsomax.Module.MonitorCore.Models.DatabaseManagement
{
    public class ConfigurationType : EntityBase
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<DatabaseSystem> DatabaseSystem { get; set; } = new List<DatabaseSystem>();
    }
}
