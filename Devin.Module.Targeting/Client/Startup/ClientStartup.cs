using Microsoft.Extensions.DependencyInjection;
using Oqtane.Services;
using Devin.Module.Targeting.Services;

namespace Devin.Module.Targeting.Startup
{
    public class ClientStartup : IClientStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ITargetingService, TargetingService>();
        }
    }
}
