using DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BIL.Extensions
{
    public static class Extentions
    {
        public static IServiceCollection SetUpAppDependencies(this IServiceCollection services,
            string connectionString)
        {
            services.AddDbContext<DataContext>(x => x.UseSqlServer(connectionString, b => b.MigrationsAssembly("api")));

            return services;
        }
    }
}