using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Oqtane.Infrastructure;
using codeXpert.Module.FAQ.Repository;
using codeXpert.Module.FAQ.Services;

namespace codeXpert.Module.FAQ.Startup
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
            services.AddTransient<IFAQService, ServerFAQService>();
            services.AddDbContextFactory<FAQContext>(opt => { }, ServiceLifetime.Transient);
        }
    }
}
