using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Itsomax.Data.Infrastructure.Models;

namespace Itsomax.Module.MonitorCore.Models.DatabaseManagement
{
    public class DatabaseEnvironment : EntityBase
    {

        public DatabaseEnvironment(long id)
        {
            Id = id;
        }
        
        [MaxLength(200)]
        public string DatabaseEnvironmentName { get; set; }
        public IList<DatabaseSystem> DatabaseSystem { get; set; } = new List<DatabaseSystem>();
    }
}