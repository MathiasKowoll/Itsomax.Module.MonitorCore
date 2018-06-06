namespace Itsomax.Module.MonitorCore.Models.DatabaseManagement
{
    public class VendorConfiguration
    {
        public long VendorId { get; set; }
        public Vendor Vendor { get; set; }
        public long ConfigurationTypeId { get; set; }
        public ConfigurationType ConfigurationType { get; set; }
    }
}