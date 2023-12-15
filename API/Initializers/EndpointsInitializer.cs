using API.SignalR;
using Microsoft.AspNetCore.Builder;

namespace API.Initializers;

public static class EndpointsInitializer
{
    public static WebApplication ConfigureEndpoints(this WebApplication app)
    {
        app.MapControllers();

        app.MapHub<OnlineHub>("hubs/online");
        app.MapHub<MessageHub>("hubs/message");
        app.MapFallbackToController("Index", "Fallback");

        return app;
    }
}
