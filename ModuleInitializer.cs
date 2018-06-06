using Itsomax.Data.Infrastructure;
using Itsomax.Module.MonitorCore.Data;
using Itsomax.Module.MonitorCore.Interfaces;
using Itsomax.Module.MonitorCore.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Itsomax.Module.MonitorCore
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
			serviceCollection.AddScoped<IMonitor, MonitorServices>();
            serviceCollection.AddScoped<IMonitorCoreRepository, MonitorCoreRepository>();
        }
    }
}