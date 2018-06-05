using System.ComponentModel.DataAnnotations;

namespace Itsomax.Module.MonitorCore.Models.DatabaseManagement
{
    public class Instance : ServiceInstance
    {

        [Required]
        public long ServiceId { get; set; }
        public Service Service { get; set; }

    }
}