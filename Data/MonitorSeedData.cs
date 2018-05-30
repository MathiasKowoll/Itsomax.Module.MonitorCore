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
                new ConfigurationType(1) {Description = "Cluster type for all databases", Name = "Cluster"},
                new ConfigurationType(2) {Description = "Standalone type for all dataases", Name = "Standalone"},
                new ConfigurationType(3)
                {
                    Description = "Always on Configuration for Sql Server 2012 and up",
                    Name = "AlwaysOn"
                }
            );
            
        }
    }
}