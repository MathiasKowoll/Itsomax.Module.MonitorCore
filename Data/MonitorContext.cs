using Itsomax.Module.Core.Data;
using Itsomax.Module.MonitorCore.Models.DatabaseManagement;
using Microsoft.EntityFrameworkCore;

namespace Itsomax.Module.MonitorCore.Data
{
    public class MonitorContext : DbContext
    {
        public MonitorContext(DbContextOptions options) : base(options)
        {
        }
        
        public DbSet<DatabaseSystem> DatabaseSystem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
        
    }
}