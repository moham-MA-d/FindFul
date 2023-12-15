using Microsoft.AspNetCore.Builder;

namespace API.Initializers;

public static class ProductionInitializer
{
    public static WebApplication ConfigureProduction(this WebApplication app)
    {
        app.UseCors(x => x
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin()
            .AllowCredentials()
            .WithOrigins("https://localhost:4200"));

        return app;
    }
}
