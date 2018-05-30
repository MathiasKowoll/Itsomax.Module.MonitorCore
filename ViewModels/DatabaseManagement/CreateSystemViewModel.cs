using System.ComponentModel.DataAnnotations;
namespace Itsomax.Module.MonitorCore.ViewModels.DatabaseManagement
{
    public class CreateSystemViewModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool Active { get; set; }
        [Required]
        public long VendorId { get; set; }
        [Required]
        public long ConfigTypeId { get; set; }
    }
}
