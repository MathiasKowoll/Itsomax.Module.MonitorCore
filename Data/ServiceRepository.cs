using System;
using System.Collections.Generic;
using System.Linq;
using Itsomax.Module.Core.Data;
using Itsomax.Module.MonitorCore.Models.DatabaseManagement;
using Itsomax.Module.MonitorCore.ViewModels.DatabaseManagement;
using Microsoft.EntityFrameworkCore;

namespace Itsomax.Module.MonitorCore.Data
{
    public class ServiceRepository : Repository<Service>,IServiceRepository
    {
        public ServiceRepository(ItsomaxDbContext context) : base(context){}

        public Service GetServiceByName(string name)
        {
            return Context.Set<Service>().FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase));
        }

        public byte[] SetPassword(string password)
        {
            byte[] setPass = null;
            var query = "select * from \"MonitorCore\".\"SetPassword\"('" + password + "')";
            using (var command = Context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                Context.Database.OpenConnection();
                var result = command.ExecuteReader();
                while (result.Read())
                {
                    setPass = (byte[])result[0];
                }
                Context.Database.CloseConnection();
            }
            
            return setPass;
        }

        public string GetPassword(byte[] password)
        {
            string stringPassword = null;
            var query = "select * from \"MonitorCore\".\"GetPassword\"('"+password+"')";
            using (var command = Context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                Context.Database.OpenConnection();
                var result = command.ExecuteReader();
                while (result.Read())
                {
                    stringPassword = (string)result[0];
                }
                Context.Database.CloseConnection();
            }

            return stringPassword;

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

    }
}