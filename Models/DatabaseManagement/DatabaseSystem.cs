using System;
using System.Collections.Generic;
using Itsomax.Data.Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace Itsomax.Module.MonitorCore.Models.DatabaseManagement
{
    public class DatabaseSystem : EntityBase
    {
        public DatabaseSystem()
        {
            UpdatedOn = DateTimeOffset.Now;
            CreatedOn = DateTimeOffset.Now;
        }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool Active { get; set; }
        [Required]
        public long VendorId { get; set; }
        public Vendor Vendor { get; set; }
        public long ConfigurationTypeId { get; set; }
        public ConfigurationType ConfigurationType { get; set; }
        public long DataBaseEnvironmentId { get; set; }
        public DatabaseEnvironment DatabaseEnvironment { get; set; }
        public IList<Service> Service { get; set; } = new List<Service>();
        public DateTimeOffset CreatedOn { get; }
        public DateTimeOffset UpdatedOn { get; set; }

    }
}
