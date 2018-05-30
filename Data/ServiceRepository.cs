using System;
using System.Linq;
using Itsomax.Module.Core.Data;
using Itsomax.Module.MonitorCore.Models.DatabaseManagement;

namespace Itsomax.Module.MonitorCore.Data
{
    public class ServiceRepository : Repository<Service>,IServiceRepository
    {
        public ServiceRepository(ItsomaxDbContext context) : base(context){}

        public Service GetServiceByName(string name)
        {
            return Context.Set<Service>().FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase));
        }

    }
}