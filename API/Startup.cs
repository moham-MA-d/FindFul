using API.Extensions;
using API.Helpers;
using API.Helpers.HealthChecks;
using API.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Linq;

namespace API
{
    public class Startup
    {
        // With this Interface we have access to our config that stored in appsetting.json file
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }

        //access to appsetting.json

        // This method gets called by the runtime. This method add services to the container.
        // Dependency Injection Container (When we want to make a class or service available to other parts of the app)
        // Ordering is not important in this function
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationServices(_config);
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // All things in here are middleware (is a software to interact with request during http pipeline)
        // Ordering is important because this is middleware for requests.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            //it is only for development and has a lot of security issues.
            //AllowAnyMethod(): get, post , put and ...
            //AllowAnyHeader() : Authentication and ...
            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()/*.WithOrigins("http://localhost:4200")*/);

            app.UseAuthentication();
            app.UseAuthorization();

            if (env.IsDevelopment())
            {

                var swaggerOption = new SwaggerOptions();
                _config.GetSection(nameof(SwaggerOptions)).Bind(swaggerOption);

                app.UseSwagger(option =>
                {
                    option.RouteTemplate = swaggerOption.JsonRoute;
                });
                app.UseDeveloperExceptionPage();

                app.UseSwaggerUI(options => options.SwaggerEndpoint(swaggerOption.UIEndpoint, swaggerOption.Description));
            }

            //map controller endpoint to application so our app knows how to route requests
            app.UseEndpoints(endpoints =>
            {
                //Look at our controller to find endpoint for example [HttpGet] is an endpoint
                endpoints.MapControllers();
            });
        }
    }
}
