using System.Collections.Generic;
using Itsomax.Data.Infrastructure.Data;
using Itsomax.Module.MonitorCore.Models.DatabaseManagement;
using Itsomax.Module.MonitorCore.ViewModels.DatabaseManagement;

namespace Itsomax.Module.MonitorCore.Data
{
    public interface IServiceRepository : IRepository<Service>
    {
        IEnumerable<ServiceListViewModel> GetServicesList();
        EditServiceViewModel GetServiceForEdit(long id);
    }
}