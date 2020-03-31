using Microsoft.Extensions.Configuration;

namespace CopaFilmes.Common.WebApi.ApiConfig
{
    public abstract class StartupApiBase<T>
    {
        public IConfiguration Configuration { get; }

        public string Namespace
            => typeof(T).Namespace;

        public StartupApiBase(IConfiguration configuration)
            => Configuration = configuration;
    }
}
