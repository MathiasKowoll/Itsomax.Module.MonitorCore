using Itsomax.Module.MonitorCore.Models;
using Itsomax.Module.MonitorCore.Models.DatabaseManagement;
using Microsoft.EntityFrameworkCore;

namespace Itsomax.Module.MonitorCore.Data
{
    public static class MonitorSeedData
    {
        public static void SeedData(ModelBuilder builder)
        {
            builder.Entity<Vendor>().HasData(
                new Vendor(1) {Description = "Microsoft vendor for Sql Server", Name = "Sql Server"},
                new Vendor(2) {Description = "EnterpriseDB Vender for Postgres", Name = "PosgreSQL"},
                new Vendor(3) {Description = "SAP vendor for Sybase IQ", Name = "Sybase IQ"},
                new Vendor(4) {Description = "Sap Vendor for Sybase ASE", Name = "Sybase Ase"}
            );
            
            builder.Entity<ConfigurationType>().HasData(
                new ConfigurationType(1) {Description = "Standalone type for all dataases", Name = "Standalone"},
                new ConfigurationType(2)
                {
                    Description = "Always on Configuration for Sql Server 2012 and up",
                    Name = "AlwaysOn"
                },
                new ConfigurationType(3) {Description = "Mirroring for Sql Server",Name = "Mirroring"},
                new ConfigurationType(4) {Description = "Transaction Replication for Sql Server",Name = "Transaction Log"},
                new ConfigurationType(5) {Description = "Failover Cluster for Sql Server",Name = "Failover Cluster"},
                new ConfigurationType(6) {Description = "Cluster active active all Databases",Name = "Cluster Active-Active"},
                new ConfigurationType(7) {Description = "Cluster active pasive all Databases",Name = "Cluster Active-Pasive"}
            );

            builder.Entity<VendorConfiguration>().HasData(
                new VendorConfiguration {VendorId = 1, ConfigurationTypeId = 1},
                new VendorConfiguration {VendorId = 1, ConfigurationTypeId = 2},
                new VendorConfiguration {VendorId = 1, ConfigurationTypeId = 3},
                new VendorConfiguration {VendorId = 1, ConfigurationTypeId = 4},
                new VendorConfiguration {VendorId = 1, ConfigurationTypeId = 5},
                new VendorConfiguration {VendorId = 2, ConfigurationTypeId = 1},
                new VendorConfiguration {VendorId = 2, ConfigurationTypeId = 6},
                new VendorConfiguration {VendorId = 2, ConfigurationTypeId = 7},
                new VendorConfiguration {VendorId = 3, ConfigurationTypeId = 1},
                new VendorConfiguration {VendorId = 3, ConfigurationTypeId = 6},
                new VendorConfiguration {VendorId = 3, ConfigurationTypeId = 7},
                new VendorConfiguration {VendorId = 4, ConfigurationTypeId = 1},
                new VendorConfiguration {VendorId = 4, ConfigurationTypeId = 6},
                new VendorConfiguration {VendorId = 4, ConfigurationTypeId = 7}
                
            );

        }
    }
}