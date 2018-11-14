using System.Collections.Generic;
using Itsomax.Data.Infrastructure.Data;
using Itsomax.Module.Core.Models;
using Itsomax.Module.Core.ViewModels;
using Itsomax.Module.MonitorCore.Models.DatabaseManagement;
using Itsomax.Module.MonitorCore.ViewModels.DatabaseManagement;

namespace Itsomax.Module.MonitorCore.Data
{
    public interface IMonitorCoreRepository : IRepository<Entity>
    {
        ConfigurationType GetConfigurationTypeByName(string name);
        IList<GenericSelectList> GetConfigurationForVendor(long vendorId);
        IEnumerable<SystemListViewModel> GetSystemListViewModels();
        EditSystemViewModel GetSystemsEditViewModel(long id);
        IEnumerable<ServiceListViewModel> GetServicesList(long? id);
        EditServiceViewModel GetServiceForEdit(long id);
        Vendor GetVendorByName(string name);
    }
}