using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using Oqtane.Services;

namespace Hexstor.Module.Template.Client.Services
{
    public class Startup : IClientStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMudServices();
    }
    }
}
