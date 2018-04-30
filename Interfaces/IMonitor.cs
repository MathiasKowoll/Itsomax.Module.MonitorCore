using Itsomax.Module.MonitorCore.Models.DatabaseManagement;
using Itsomax.Module.MonitorCore.ViewModels.DatabaseManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Itsomax.Module.MonitorCore.Interfaces
{
    public interface IMonitor
    {
        bool CreateSystem(CreateSystemViewModel model, string Username);
        IEnumerable<SystemListViewModel> GetSystemList(string userName);
        EditSystemViewModel GetSystemForEdit(long Id, string userName);
        DatabaseSystem GetSystem(long Id, string userName);
        DatabaseSystem GetSystem(long Id);
        bool EditSystem(EditSystemViewModel model, string userName);
        bool DeleteSystem(long Id, string userName);
        bool DisableEnableSystem(long Id, string userName);
    }
}
