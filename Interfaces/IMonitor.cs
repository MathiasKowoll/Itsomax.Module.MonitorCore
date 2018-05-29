using Itsomax.Module.MonitorCore.Models.DatabaseManagement;
using Itsomax.Module.MonitorCore.ViewModels.DatabaseManagement;
using System.Collections.Generic;
using System.Threading.Tasks;
using Itsomax.Module.Core.Extensions;

namespace Itsomax.Module.MonitorCore.Interfaces
{
    public interface IMonitor
    {
        Task<SystemSucceededTask> CreateSystem(CreateSystemViewModel model, string userName);
        IEnumerable<SystemListViewModel> GetSystemList(string userName);
        EditSystemViewModel GetSystemForEdit(long id, string userName);
        DatabaseSystem GetSystem(long id, string userName);
        DatabaseSystem GetSystem(long id);
        bool EditSystem(EditSystemViewModel model, string userName);
        bool DeleteSystem(long id, string userName);
        bool DisableEnableSystem(long id, string userName);
    }
}
