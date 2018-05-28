using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata;
using Itsomax.Data.Infrastructure.Data;
using Itsomax.Module.MonitorCore.Models.DatabaseManagement;

//using Itsomax.Module.MonitorCore.Models;


namespace Itsomax.Module.MonitorCore.Data
{
    class MonitorCustomModelBuilder : ICustomModelBuilder
    {
        public void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DatabaseSystem>(o =>
            {
                o.HasOne(x => x.Vendor).WithMany(u => u.DatabaseSystem).HasForeignKey(x => x.VendorId);
                o.HasOne(x => x.ConfigurationType).WithMany(x => x.DatabaseSystem)
                    .HasForeignKey(x => x.ConfigurationTypeId);
            });
            modelBuilder.Entity<Service>(o =>
            {
                o.HasOne(x => x.DatabaseSystem).WithMany(x => x.Service).HasForeignKey(x => x.DatabaseSystemId);
            });
        }
    }
}
