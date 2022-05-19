using API.Extensions;
using API.Helpers;
using API.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

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

        // This method gets called by the runtime. Use this method to add services to the container.
        // Dependence Injection Container (When we want to make a class or service available to other parts of the app)
        // Ordering is not important in this function
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationServices(_config);

            services.AddControllers();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Findful API", Version = "v1" });
                options.CustomSchemaIds(type => type.ToString());
            });
            services.AddCors();

            services.AddIdentityServices(_config);

            services.AddSingleton(_ => _config);
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // All things in here are middleware (is a software to interact with request during http pipeline)
        // Ordering is important because this is middleware for requests.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();

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

            app.UseHttpsRedirection();

            app.UseRouting();

            //it is only for development and has a lot of security issues.
            //AllowAnyMethod(): get, post , put and ...
            //AllowAnyHeader() : Authentication and ...
            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()/*.WithOrigins("http://localhost:4200")*/);

            app.UseAuthentication();
            app.UseAuthorization();

            //map controller endpoint to application so our app knows how to route requests
            app.UseEndpoints(endpoints =>
            {
                //Look at our controller to find endpoint for example [HttpGet] is an endpoint
                endpoints.MapControllers();
            });
        }
    }
}
