using Itsomax.Module.MonitorCore.Models.DatabaseManagement;
using Microsoft.EntityFrameworkCore;

namespace Itsomax.Module.MonitorCore.Data
{
    public static class MonitorSeedData
    {
        public static void SeedData(ModelBuilder builder)
        {
            builder.Entity<Vendor>().HasData(
                new Vendor(1) {Description = "Microsoft vendor for Sql Server", Name = "Sql Server",Driver = "ODBC Driver 17 for SQL Server"},
                new Vendor(2) {Description = "EnterpriseDB Vendor for Postgres", Name = "PostgreSQL",Driver = "ODBC for PostgreSQL"},
                new Vendor(3) {Description = "SAP vendor for Sybase IQ", Name = "Sybase IQ",Driver = "SQL Anywhere 17"},
                new Vendor(4) {Description = "SAP Vendor for Sybase ASE", Name = "Sybase Ase",Driver = "Adaptive Server Enterprise"}
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
                new ConfigurationType(7) {Description = "Cluster active passive all Databases",Name = "Cluster Active-Passive"}
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
            builder.Entity<DatabaseEnvironment>().HasData(
                new DatabaseEnvironment(1) {DatabaseEnvironmentName = "Production"},
                new DatabaseEnvironment(2) {DatabaseEnvironmentName = "Development"},
                new DatabaseEnvironment(3) {DatabaseEnvironmentName = "Quality Assurance (QA)"},
                new DatabaseEnvironment(4) {DatabaseEnvironmentName = "Staging"}
            );

        }
    }
}