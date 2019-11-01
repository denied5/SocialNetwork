
using BIL.Helpers;
using BIL.Services;
using BIL.Services.Interrfaces;
using DAL.Data;
using DAL.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
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

        public static void AddPagination(this HttpResponse response, int currentPage,
            int itemsPerPage, int totalItems, int totalPages)
        {
            var paginationHeader = new PaginationHeader(currentPage, itemsPerPage, totalItems, totalPages);
            response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationHeader));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }

        public static IServiceCollection SetUpScopes(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IFrienshipService, FrienshipService>();
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