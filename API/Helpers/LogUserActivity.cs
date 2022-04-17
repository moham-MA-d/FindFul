using API.Extensions;
using Core.IRepositories.User;
using Core.IService.User;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        //Action filters allow us to do something even before the request is executing or after the request is executed.

        //context: the action that is executing .
        //next: what's going to happen after the action is executed.
            //we use it to execute the action and do sth after this is executed.

        //In this Senario we are going to access `context` after `next` is executed
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //resultContext: It is the context when the action is executed
            var resultContext = await next();

            if (!resultContext.HttpContext.User.Identity.IsAuthenticated) return;

            var userId = resultContext.HttpContext.User.GetUserId();
            var userService = resultContext.HttpContext.RequestServices.GetService<IUserService>();
            var user = await userService.GetUserByIdAsync(userId);

            user.LastActivity = System.DateTime.Now;

            await userService.SaveChangesAsync();
        }
    }
}
