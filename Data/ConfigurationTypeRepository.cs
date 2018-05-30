using System;
using System.Linq;
using Itsomax.Module.Core.Data;
using Itsomax.Module.MonitorCore.Models.DatabaseManagement;

namespace Itsomax.Module.MonitorCore.Data
{
    public class ConfigurationTypeRepository : Repository<ConfigurationType>,IConfigurationTypeRepository
    {
        public ConfigurationTypeRepository(ItsomaxDbContext context) : base(context){}

        public ConfigurationType GetConfigurationTypeByName(string name)
        {
            return Context.Set<ConfigurationType>().FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase));
        }
        
    }
}