using System;
using System.IO;
using System.Reflection;
using API.Extensions;
using API.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Service.Helpers;
using Swashbuckle.AspNetCore.Filters;

namespace API.ServiceInstallers
{
    public class BasicInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("V1", new OpenApiInfo { Title = "Findful API", Version = "V1" });
                //Add filters
                options.ExampleFilters();
                options.CustomSchemaIds(type => type.ToString());

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Findful Authorization using the bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
            //add a registered of field
            services.AddSwaggerExamplesFromAssemblyOf<Startup>();
            

            services.AddCors();

            services.AddIdentityServices(configuration);

            services.AddSingleton(_ => configuration);

            services
                .AddMvc(opt => { opt.EnableEndpointRouting = false; });
            
            services.AddSingleton(sp => sp.GetRequiredService<ILoggerFactory>()
                .CreateLogger("DefaultLogger"));
            services.AddScoped<LogUserActivity>();
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            services.AddSignalR(e => {
                //e.MaximumReceiveMessageSize = 102400000;
                //e.EnableDetailedErrors = true;
            });        
        }
    }
}
