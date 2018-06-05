using System;
using System.Collections.Generic;
using System.Linq;
using Itsomax.Module.Core.Data;
using Itsomax.Module.Core.ViewModels;
using Itsomax.Module.MonitorCore.Models;
using Itsomax.Module.MonitorCore.Models.DatabaseManagement;

namespace Itsomax.Module.MonitorCore.Data
{
    public class ConfigurationTypeRepository : Repository<ConfigurationType>,IConfigurationTypeRepository
    {
        public ConfigurationTypeRepository(ItsomaxDbContext context) : base(context){}

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
        
    }
}