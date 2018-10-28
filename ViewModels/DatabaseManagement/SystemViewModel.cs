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
        [Required]
        public long EnvironmentId { get; set; }
    }
    public class EditSystemViewModel
    {
        [Required]
        public long Id { get; set; }
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
        [Required]
        public long EnvironmentId { get; set; }
    }
    
    public class SystemListViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public string VendorName { get; set; }
        public string ConfigurationType { get; set; }
        public string EnvironmentName { get; set; }
    }
}
