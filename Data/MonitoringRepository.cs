using System.Reflection.Metadata.Ecma335;
using Itsomax.Module.Core.Data;
using Itsomax.Module.Core.Models;

namespace Itsomax.Module.MonitorCore.Data
{
    public class MonitoringRepository : Repository<Entity>,IMonitoringRepository
    {
        public MonitoringRepository(ItsomaxDbContext context) : base(context){}

    }
}