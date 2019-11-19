
using BIL.Helpers;
using BIL.Services;
using BIL.Services.Interrfaces;
using DAL.Data;
using DAL.Models;
using DAL.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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

            //---------------------------
            IdentityBuilder builder = services.AddIdentityCore<User>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 4;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
            });

            builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);
            builder.AddEntityFrameworkStores<DataContext>();
            builder.AddRoleValidator<RoleValidator<Role>>();
            builder.AddRoleManager<RoleManager<Role>>();
            builder.AddSignInManager<SignInManager<User>>(); 



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
            services.AddScoped<IMessagesService, MessagesService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ILikeService, LikeService>();
            services.AddScoped<UpdateUserActivityFilter>();
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