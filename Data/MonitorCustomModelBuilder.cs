using Microsoft.EntityFrameworkCore;
using Itsomax.Data.Infrastructure.Data;
using Itsomax.Module.MonitorCore.Entities;
using Itsomax.Module.MonitorCore.Models.DatabaseManagement;

namespace Itsomax.Module.MonitorCore.Data
{
    public class MonitorCustomModelBuilder : ICustomModelBuilder
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

            modelBuilder.Entity<Instance>(o =>
            {
                o.HasOne(x => x.Service).WithMany(x => x.Instance).HasForeignKey(x => x.ServiceId);
            });

            modelBuilder.Entity<VendorConfiguration>(o =>
            {
                o.HasOne(x => x.ConfigurationType).WithMany(x => x.VendorConfiguration)
                    .HasForeignKey(x => x.ConfigurationTypeId);
                o.HasOne(x => x.Vendor).WithMany(x => x.VendorConfiguration).HasForeignKey(x => x.VendorId);
                o.ToTable("VendorConfiguration", "MonitorCore");
                o.HasKey(x => new {x.ConfigurationTypeId, x.VendorId});

            });

            modelBuilder.Entity<ServiceStatus>(o =>
            {
                o.HasKey(x => new {x.ServiceId});
                o.ToTable("ServiceStatus", "MonitorCore");
            });

            modelBuilder.Entity<InstanceStatus>(o =>
            {
                o.HasKey(x => new {x.InstanceId});
                o.ToTable("InstanceStatus", "MonitorCore");
            });

            MonitorSeedData.SeedData(modelBuilder);
        }
    }
}
