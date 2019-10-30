
using BIL.Helpers;
using BIL.Services;
using BIL.Services.Interrfaces;
using DAL.Data;
using DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

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
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPhotoService, PhotoService>();
            return services;
        }

        public static int CalculateAge(this DateTime theDateTime)
        {
            var age = System.DateTime.Now.Year - theDateTime.Year;
            if (theDateTime.AddYears(age) > DateTime.Today)
                age--;
            return age;
        }
    }
}