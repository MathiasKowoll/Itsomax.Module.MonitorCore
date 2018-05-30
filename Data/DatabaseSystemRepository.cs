using System.Collections.Generic;
using System.Linq;
using Itsomax.Module.Core.Data;
using Itsomax.Module.MonitorCore.Models.DatabaseManagement;
using Itsomax.Module.MonitorCore.ViewModels.DatabaseManagement;


namespace Itsomax.Module.MonitorCore.Data
{
    public class DatabaseSystemRepository : Repository<DatabaseSystem>, IDatabaseSystemRepository
    {
        public DatabaseSystemRepository(ItsomaxDbContext context) : base(context)
        {
        }

        public IEnumerable<SystemListViewModel> GetSystemListViewModels()
        {
            return
                from dbs in Context.Set<DatabaseSystem>()
                join v in Context.Set<Vendor>() on dbs.VendorId equals v.Id
                join ct in Context.Set<ConfigurationType>() on dbs.ConfigurationTypeId equals ct.Id
                select new SystemListViewModel()
                {
                    Id = dbs.Id,
                    Active = dbs.Active,
                    ConfigurationType = ct.Name,
                    Description = dbs.Description,
                    Name = dbs.Name,
                    VendorName = v.Name
                };
        }
    }
}