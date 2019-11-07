using BIL.Services.Interrfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BIL.Helpers
{
    public class UpdateUserActivityFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            var userId = int.Parse(resultContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userService = resultContext.HttpContext.RequestServices.GetService<IUserService>();

            await userService.UpdateUserActivity(userId);
        }
    }
}
