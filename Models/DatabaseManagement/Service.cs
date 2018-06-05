using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Itsomax.Module.MonitorCore.Models.DatabaseManagement
{
    public class Service : ServiceInstance
    {
        [Required]
        public long DatabaseSystemId { get; set; }
        public DatabaseSystem DatabaseSystem { get; set; }
        public IList<Instance> Instance { get; set; } = new List<Instance>();
    }
}