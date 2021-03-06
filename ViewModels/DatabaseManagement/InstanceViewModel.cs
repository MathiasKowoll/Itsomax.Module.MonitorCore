using System.ComponentModel.DataAnnotations;

namespace Itsomax.Module.MonitorCore.ViewModels.DatabaseManagement
{
    public class CreateInstanceViewModel
    {
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
        [MaxLength(50)]
        [Required]
        public string Ip { get; set; }
        [MaxLength(200)]
        [Required]
        public string Hostname { get; set; }
        [MaxLength(100)]
        public string Named { get; set; }
        [MaxLength(100)]
        [Required]
        public string LoginName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string LoginPassword { get; set; }
        [Required]
        public bool Active { get; set; }
        [Required]
        public long ServiceId { get; set; }
    }

    public class EditInstanceViewModel
    {
        [Required]
        public long Id { get; set; }
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
        [MaxLength(50)]
        [Required]
        public string Ip { get; set; }
        [MaxLength(200)]
        [Required]
        public string Hostname { get; set; }
        [MaxLength(100)]
        public string Named { get; set; }
        [MaxLength(100)]
        [Required]
        public string LoginName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string LoginPassword { get; set; }
        [Required]
        public bool Active { get; set; }
        [Required]
        public long ServiceId { get; set; }
    }
    
    public class InstanceListViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public string SystemName { get; set; }
        public string ServiceName { get; set; }
        public string Hostname { get; set; }
        public string UpdatedOn { get; set; }
    }
}
