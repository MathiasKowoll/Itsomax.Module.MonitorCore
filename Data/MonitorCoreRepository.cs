using System;
using System.Collections.Generic;
using System.Linq;
using Itsomax.Module.Core.Data;
using Itsomax.Module.Core.Models;
using Itsomax.Module.Core.ViewModels;
using Itsomax.Module.MonitorCore.Models;
using Itsomax.Module.MonitorCore.Models.DatabaseManagement;
using Itsomax.Module.MonitorCore.ViewModels.DatabaseManagement;

namespace Itsomax.Module.MonitorCore.Data
{
    public class MonitorCoreRepository : Repository<Entity>,IMonitorCoreRepository
    {
        public MonitorCoreRepository(ItsomaxDbContext context) : base(context){}
        
        public ConfigurationType GetConfigurationTypeByName(string name)
        {
            return Context.Set<ConfigurationType>().FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase));
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
            //list.Add(new GenericSelectList {Id = 0,Name = "Select Configuration",Selected = true});
            return list;
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

        public EditSystemViewModel GetSystemsEditViewModel(long id)
        {
            return 
                (from dbs in Context.Set<DatabaseSystem>()
                    join v in Context.Set<Vendor>() on dbs.VendorId equals v.Id
                    join ct in Context.Set<ConfigurationType>() on dbs.ConfigurationTypeId equals ct.Id
                    where dbs.Id == id
                    select new EditSystemViewModel
                    {
                    
                        Id = dbs.Id,
                        Active = dbs.Active,
                        ConfigTypeId = ct.Id,
                        Description = dbs.Description,
                        Name = dbs.Name,
                        VendorId = v.Id
                    }).SingleOrDefault();
        }
        
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
        
        public Vendor GetVendorByName(string name)
        {
            return Context.Set<Vendor>().FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase));
        }

    }
}