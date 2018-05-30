using System;
using System.Linq;
using Itsomax.Module.Core.Data;
using Itsomax.Module.MonitorCore.Models.DatabaseManagement;

namespace Itsomax.Module.MonitorCore.Data
{
    public class VendorRepository : Repository<Vendor>,IVendorRepository
    {
        public VendorRepository(ItsomaxDbContext context) : base(context)
        {
        }

        public Vendor GetVendorByName(string name)
        {
            return Context.Set<Vendor>().FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase));
        }

    }
}