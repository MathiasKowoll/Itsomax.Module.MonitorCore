using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Itsomax.Module.MonitorCore.Entities
{
    public class ServiceStatus
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ServiceId { get; set; }
        public bool Status { get; set; }
        public DateTimeOffset Timestamp { get; set; }
    }
}