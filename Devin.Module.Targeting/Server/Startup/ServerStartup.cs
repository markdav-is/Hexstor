using Microsoft.AspNetCore.Builder; 
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Oqtane.Infrastructure;
using Devin.Module.Targeting.Repository;
using Devin.Module.Targeting.Services;

namespace Devin.Module.Targeting.Startup
{
    public class ServerStartup : IServerStartup
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // not implemented
        }

        public void ConfigureMvc(IMvcBuilder mvcBuilder)
        {
            // not implemented
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ITargetingService, ServerTargetingService>();
            services.AddDbContextFactory<TargetingContext>(opt => { }, ServiceLifetime.Transient);
        }
    }
}
