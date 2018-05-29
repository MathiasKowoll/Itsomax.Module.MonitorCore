using System.Collections.Generic;
using Itsomax.Module.Core.Data;
using Itsomax.Module.MonitorCore.Models.DatabaseManagement;
using Microsoft.EntityFrameworkCore;

namespace Itsomax.Module.MonitorCore.Data
{
    public class MonitorRepository : Repository<DatabaseSystem>, IMonitorRepository
    {
        public MonitorRepository(ItsomaxDbContext context) : base(context)
        {
        }

        public DbSet<DatabaseSystem> DatabaseSystems { get; set; }

        public IEnumerable<DatabaseSystem> GetAllDatabaseSystems()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<DatabaseSystem> GetDatabaseSystemsById(long id)
        {
            throw new System.NotImplementedException();
        }

        public ItsomaxDbContext ItsomaxDbContext
        {
            get { return Context as ItsomaxDbContext; }
        }
    }
}