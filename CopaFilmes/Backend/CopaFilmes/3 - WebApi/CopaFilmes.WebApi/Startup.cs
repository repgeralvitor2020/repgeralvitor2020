using CopaFilmes.Common.WebApi.ApiConfig;
using CopaFilmes.Common.WebApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CopaFilmes.WebApi
{
    public class Startup : StartupApiBase<Startup>
    {
        public Startup(IConfiguration configuration) :
            base(configuration) { }

        public void ConfigureServices(IServiceCollection services)
            => services.Config(Configuration, Namespace);

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
            => app.Config(env, Namespace);
    }
}
