using System;
using System.Collections.Generic;
using System.Linq;
using Itsomax.Module.Core.Data;
using Itsomax.Module.MonitorCore.Models.DatabaseManagement;
using Itsomax.Module.MonitorCore.ViewModels.DatabaseManagement;

namespace Itsomax.Module.MonitorCore.Data
{
    public class ServiceRepository : Repository<Service>,IServiceRepository
    {
        public ServiceRepository(ItsomaxDbContext context) : base(context){}

        public Service GetServiceByName(string name)
        {
            return Context.Set<Service>().FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase));
        }

        public IEnumerable<ServiceListViewModel> GetServicesList()
        {
            return
                from s in Context.Set<Service>()
                join d in Context.Set<DatabaseSystem>() on s.DatabaseSystemId equals d.Id
                select new ServiceListViewModel
                {
                    Id = s.Id,
                    SystemName = d.Name,
                    Name = s.Name,
                    Hostname = s.Hostname,
                    Active = s.Active,
                    UpdatedOn = s.UpdatedOn.ToString("yyyy/MM/dd HH:mm:ss zz")
                };
        }

        public EditServiceViewModel GetServiceForEdit(long id)
        {
            return 
                (from s in Context.Set<Service>()
                where s.Id == id
                select new EditServiceViewModel
                {
                    Id = s.Id,
                    DatabaseSystemId = s.DatabaseSystemId,
                    Active = s.Active,
                    Hostname = s.Hostname,
                    Ip = s.Ip,
                    LoginName = s.LoginName,
                    LoginPassword = "ChangeMe".ToUpper(),
                    Name = s.Name,
                    Named = s.Named
                }).FirstOrDefault();


        }

    }
}