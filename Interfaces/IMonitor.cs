using Itsomax.Module.MonitorCore.ViewModels.DatabaseManagement;
using System.Collections.Generic;
using System.Threading.Tasks;
using Itsomax.Module.Core.Extensions;
using Itsomax.Module.Core.ViewModels;
using Itsomax.Module.MonitorCore.Models.DatabaseManagement;

namespace Itsomax.Module.MonitorCore.Interfaces
{
    public interface IMonitor
    {
        Task<SystemSucceededTask> CreateSystem(CreateSystemViewModel model, string userName);
        IEnumerable<SystemListViewModel> GetSystemList(string userName);
        EditSystemViewModel GetSystemForEdit(long id, string userName);
        Task<SystemSucceededTask> EditSystem(EditSystemViewModel model, string userName);
        bool DeleteSystem(long id, string userName);
        bool DisableEnableSystem(long id, string userName);
        IList<GenericSelectList> ConfigurationTypeSelectList(long id);
        IList<GenericSelectList> VendorSelectList(long id);
        IList<GenericSelectList> DatabaseSystemList(long id);
        DatabaseSystem GetDatabaseSystemById(long id, string userName);
        IEnumerable<ServiceListViewModel> GetServicesList(string userName);
        Service GetServiceByName(long id, string userName);
        Task<SystemSucceededTask> CreateService(CreateServiceViewModel model, string userName);
        Task<SystemSucceededTask> EditService(EditServiceViewModel model, string userName);
    }
}
