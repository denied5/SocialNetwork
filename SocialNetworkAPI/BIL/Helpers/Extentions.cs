using BIL.Services;
using BIL.Services.Interrfaces;
using DAL.Data;
using DAL.UnitOfWork;
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

        public static IServiceCollection SetUpScopes(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}