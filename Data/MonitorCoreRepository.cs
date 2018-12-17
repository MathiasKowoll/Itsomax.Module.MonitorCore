using System;
using System.Collections.Generic;
using System.Linq;
using Itsomax.Module.Core.Data;
using Itsomax.Module.Core.Models;
using Itsomax.Module.Core.ViewModels;
using Itsomax.Module.MonitorCore.Models.DatabaseManagement;
using Itsomax.Module.MonitorCore.ViewModels.DatabaseManagement;

namespace Itsomax.Module.MonitorCore.Data
{
    public class MonitorCoreRepository : Repository<Entity>,IMonitorCoreRepository
    {
        public MonitorCoreRepository(ItsomaxDbContext context) : base(context){}
        
        public ConfigurationType GetConfigurationTypeByName(string name)
        {
            return Context.Set<ConfigurationType>()
                .FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase));
        }
        
        
        public IList<GenericSelectList> GetConfigurationForVendor(long vendorId)
        {
            var list = 
                (from vc in Context.Set<VendorConfiguration>().Where(x => x.VendorId == vendorId)
                    join c in Context.Set<ConfigurationType>() on vc.ConfigurationTypeId equals c.Id
                    select new GenericSelectList
                    {
                        Id = c.Id,
                        Name = c.Name
                    }).ToList();
            return list;
        }

        public bool IsSystemStandalone(long systemId)
        {
            var list = (from s in Context.Set<DatabaseSystem>().Where(x => x.Id == systemId)
                join c in Context.Set<ConfigurationType>().Where(x => x.Name == "Standalone") on s.ConfigurationTypeId
                    equals c.Id
                select c.Name).FirstOrDefault();
            return list != null && list.Any();
        }
        
        public IEnumerable<SystemListViewModel> GetSystemListViewModels()
        {
            return
                from dbs in Context.Set<DatabaseSystem>()
                join v in Context.Set<Vendor>() on dbs.VendorId equals v.Id
                join ct in Context.Set<ConfigurationType>() on dbs.ConfigurationTypeId equals ct.Id
                join de in Context.Set<DatabaseEnvironment>() on dbs.DataBaseEnvironmentId equals de.Id into des
                from de in des.DefaultIfEmpty()
                select new SystemListViewModel()
                {
                    Id = dbs.Id,
                    Active = dbs.Active,
                    ConfigurationType = ct.Name,
                    Description = dbs.Description,
                    Name = dbs.Name,
                    VendorName = v.Name,
                    EnvironmentName = de.DatabaseEnvironmentName ?? "No Environment Assign"
                };
        }
        

        public EditSystemViewModel GetSystemsEditViewModel(long id)
        {
            return 
                (from dbs in Context.Set<DatabaseSystem>()
                    join v in Context.Set<Vendor>() on dbs.VendorId equals v.Id
                    join ct in Context.Set<ConfigurationType>() on dbs.ConfigurationTypeId equals ct.Id
                    join de in Context.Set<DatabaseEnvironment>() on dbs.DataBaseEnvironmentId equals  de.Id into denv
                    from de in denv.DefaultIfEmpty()
                    where dbs.Id == id
                    select new EditSystemViewModel
                    {
                    
                        Id = dbs.Id,
                        Active = dbs.Active,
                        ConfigTypeId = ct.Id,
                        Description = dbs.Description,
                        Name = dbs.Name,
                        VendorId = v.Id,
                        EnvironmentId  = dbs.DataBaseEnvironmentId ?? 0
                    }).SingleOrDefault();
        }
        
        public Service GetServiceByName(string name)
        {
            return Context.Set<Service>()
                .FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase));
        }

        public IEnumerable<ServiceListViewModel> GetServicesList(long? id)
        {

            if (id == null)
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
            return
                from s in Context.Set<Service>()
                join d in Context.Set<DatabaseSystem>() on s.DatabaseSystemId equals d.Id
                where d.Id == id.Value
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
        
        public IEnumerable<InstanceListViewModel> GetInstanceList(long? id)
        {

            if (id == null)
            {
                return
                    from s in Context.Set<Service>()
                    join d in Context.Set<DatabaseSystem>() on s.DatabaseSystemId equals d.Id
                    join i in Context.Set<Instance>() on s.Id equals i.ServiceId
                    select new InstanceListViewModel
                    {
                        Id = i.Id,
                        SystemName = d.Name,
                        ServiceName = s.Name,
                        Name = i.Name,
                        Hostname = i.Hostname,
                        Active = i.Active,
                        UpdatedOn = i.UpdatedOn.ToString("yyyy/MM/dd HH:mm:ss zz")
                    };
            }
            return
                from s in Context.Set<Service>()
                join d in Context.Set<DatabaseSystem>() on s.DatabaseSystemId equals d.Id
                join i in Context.Set<Instance>() on s.Id equals i.ServiceId
                where s.Id == id.Value
                select new InstanceListViewModel
                {
                    Id = i.Id,
                    SystemName = d.Name,
                    ServiceName = s.Name,
                    Name = i.Name,
                    Hostname = i.Hostname,
                    Active = i.Active,
                    UpdatedOn = i.UpdatedOn.ToString("yyyy/MM/dd HH:mm:ss zz")
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
        
        public EditInstanceViewModel GetInstanceForEdit(long id)
        {
            return 
                (from i in Context.Set<Instance>()
                    where i.Id == id
                    select new EditInstanceViewModel
                    {
                        Id = i.Id,
                        ServiceId = i.ServiceId,
                        Active = i.Active,
                        Hostname = i.Hostname,
                        Ip = i.Ip,
                        LoginName = i.LoginName,
                        LoginPassword = "ChangeMe".ToUpper(),
                        Name = i.Name,
                        Named = i.Named
                    }).FirstOrDefault();


        }
        
        public Vendor GetVendorByName(string name)
        {
            return Context.Set<Vendor>()
                .FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase));
        }

    }
}