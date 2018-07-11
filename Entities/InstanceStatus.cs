using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Itsomax.Module.MonitorCore.Entities
{
    public class InstanceStatus
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long InstanceId { get; set; }
        public bool Status { get; set; }
        public DateTimeOffset Timestamp { get; set; }
    }
}