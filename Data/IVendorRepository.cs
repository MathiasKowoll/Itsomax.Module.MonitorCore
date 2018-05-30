using Itsomax.Data.Infrastructure.Data;
using Itsomax.Module.MonitorCore.Models.DatabaseManagement;

namespace Itsomax.Module.MonitorCore.Data
{
    public interface IVendorRepository : IRepository<Vendor>
    {
        Vendor GetVendorByName(string name);
    }
}