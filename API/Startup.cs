using API.Findful.Extentions;
using API.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace API
{
    public class Startup
    {
        // With this Interface we have access to our config that stired in appsetting.json file
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }

        //access to appsetting.json

        // This method gets called by the runtime. Use this method to add services to the container.
        // Dependebcy Injection Container (When we want to make a class or service availble to other parts of the app)
        // Ordering is not important in this function
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationServices(_config);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApplication1", Version = "v1" });
            });
            services.AddCors();

            services.AddIdentityServices(_config);

            services.AddSingleton(_ => _config);

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // All things in here are midleware (is a software to interact with request during http pipeline)
        // Ordering is important because this is midleware for requests.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            //     app.UseSwagger();
            //     app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplication1 v1"));
            // }

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
