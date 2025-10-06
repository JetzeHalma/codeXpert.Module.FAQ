using Microsoft.Extensions.DependencyInjection;
using Oqtane.Services;
using codeXpert.Module.FAQ.Services;

namespace codeXpert.Module.FAQ.Startup
{
    public class ClientStartup : IClientStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IFAQService, FAQService>();
        }
    }
}
