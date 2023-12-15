using API.Helpers;
using API.Helpers.HealthChecks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Serilog;
using System.Linq;

namespace API.Initializers;

public static class DevelopementInitializer
{
    public static WebApplication ConfigureDevelopement(this WebApplication app, IConfiguration config)
    {

        app.UseDefaultFiles();
        app.UseStaticFiles();

        app.UseSerilogRequestLogging();

        SwaggerSetup(app, config);

        app.UseCors(x => x
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin()
            .AllowCredentials()
            .WithOrigins("https://localhost:4200"));

        HealthCheckSetup(app);

        return app;

    }


    private static void SwaggerSetup(WebApplication app, IConfiguration config)
    {
        var swaggerOption = new SwaggerSettings();
        config.GetSection(nameof(SwaggerSettings)).Bind(swaggerOption);
        app.UseSwagger(option => { option.RouteTemplate = swaggerOption.JsonRoute; });
        app.UseDeveloperExceptionPage();
        app.UseSwaggerUI(
            options => options.SwaggerEndpoint(swaggerOption.UIEndpoint, swaggerOption.Description));
    }

    private static void HealthCheckSetup(WebApplication app)
    {
        app.UseHealthChecks("/Health", new HealthCheckOptions
        {
            // accept httpcontext and report our specific component we're going to use that for the response
            // this is a delegate that provides context (HttpContext) and a report for this check
            ResponseWriter = async (context, report) =>
            {
                context.Response.ContentType = "application/json";

                var response = new HealthCheckResponse
                {
                    Status = report.Status.ToString(),
                    Checks = report.Entries.Select(x => new HealthCheck
                    {
                        Component = x.Key, //key is the name of the thing (component) we are checking
                        Status = x.Value.Status.ToString(),
                        Description = x.Value.Description
                    }),
                    Duration = report.TotalDuration
                };

                await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
            }
        });
    }

}
