using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaQuanto.Service.Interfaces;
using TaQuanto.Service.Services;

namespace TaQuanto.Service.Helpers
{
    public static class IServiceCollectionExtencion
    {
        public static void AddService(this IServiceCollection service, IConfiguration config)
        {
            AddClassConfigJwt(service, config);
        }

        public static void AddClassConfigJwt(IServiceCollection service, IConfiguration config) 
        {
            JwtConfig jwtConfig = new JwtConfig();
            config.GetSection("JWT").Bind(jwtConfig);
            service.AddSingleton(jwtConfig);
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
        }
    }
}
