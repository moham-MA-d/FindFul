using Microsoft.AspNetCore.Builder;

namespace API.Initializers;

public static partial class MiddlewareInitializer
{
    public static WebApplication ConfigureBasicMiddlewares(this WebApplication app)
    {
        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();


        return app;
    }
}
