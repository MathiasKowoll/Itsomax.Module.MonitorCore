using System.Collections.Generic;
using Itsomax.Data.Infrastructure.Data;
using Itsomax.Module.MonitorCore.Models.DatabaseManagement;

namespace Itsomax.Module.MonitorCore.Data
{
    public interface IMonitorRepository : IRepository<DatabaseSystem>
    {
        IEnumerable<DatabaseSystem> GetAllDatabaseSystems();
        IEnumerable<DatabaseSystem> GetDatabaseSystemsById(long id);
    }
}