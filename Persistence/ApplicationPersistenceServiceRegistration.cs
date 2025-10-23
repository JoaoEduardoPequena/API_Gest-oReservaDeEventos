using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DbContexts;
using Persistence.Repositories;

namespace Persistence
{
    public static class ApplicationPersistenceServiceRegistration
    {
        public static void AddInfraPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(o => o.UseSqlServer(configuration.GetConnectionString("Connection"),
                builderOp => builderOp.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddTransient<IRepoEvento, RepoEvento>();
            services.AddTransient<IRepoPedido, RepoPedido>();
        }
    }
}
