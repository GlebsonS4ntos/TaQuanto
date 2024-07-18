using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaQuanto.Infra.Data;

namespace TaQuanto.Infra.Helpers
{
    public static class IServiceCollectionExtencion
    {
        public static void AddInfra(this IServiceCollection service, IConfiguration config)
        {
            AddClassConfigConnectionString(service, config);
            AddDbContext(service);
        }

        private static void AddDbContext(IServiceCollection service)
        {
            service.AddDbContext<TaQuantoContext>((serviceProvider, opt) =>
            {
                var connection = serviceProvider.GetRequiredService<DbConnectionConfig>();
                opt.UseSqlServer(connection.ConnectionString);
            });
        }

        private static void AddClassConfigConnectionString(IServiceCollection service, IConfiguration config) 
        { 
            DbConnectionConfig configConnection = new DbConnectionConfig();
            config.GetSection("DbConnection").Bind(configConnection);
            service.AddSingleton(configConnection);
        }

    }
}
