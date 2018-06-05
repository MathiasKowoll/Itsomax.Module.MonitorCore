using System.Collections.Generic;
using Itsomax.Data.Infrastructure.Data;
using Itsomax.Module.Core.ViewModels;
using Itsomax.Module.MonitorCore.Models.DatabaseManagement;

namespace Itsomax.Module.MonitorCore.Data
{
    public interface IConfigurationTypeRepository : IRepository<ConfigurationType>
    {
        ConfigurationType GetConfigurationTypeByName(string name);
        IList<GenericSelectList> GetConfigurationForVendor(long vendorId);
    }
}