using System.Text;
using API.Helpers;
using API.Helpers.Authorizations;
using Core.Models.Entities.User;
using Data;
using DTO.Admin;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
    public static class IdentityServiceExtension
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentityCore<AppUser>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
            })
            .AddRoles<AppRole>()
            .AddRoleManager<RoleManager<AppRole>>()
            .AddSignInManager<SignInManager<AppUser>>()
            .AddRoleValidator<RoleValidator<AppRole>>()
            .AddEntityFrameworkStores<DataContext>();

            var jwtSettings = new JwtSettings();
            configuration.Bind(nameof(jwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings); 

            var tokenValidationParameters = new TokenValidationParameters
            {
                // two below lines are for authenticate users with a valid token
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.SecretKey)),

                ValidateIssuer = false, //Our Findful.API server
                ValidateAudience = false, // Our Angular Application

                //RequireExpirationTime = true,
                //ValidateLifetime = true

            };

            services.AddSingleton(tokenValidationParameters);

            services
                .AddAuthentication(option =>
                {
                    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                // It show that how our token look like and how system should use it.
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = tokenValidationParameters;
                });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy(StaticPolicies.AdminPolicy, policy => policy.RequireRole(StaticRoles.Admin));
                opt.AddPolicy(StaticPolicies.ModeratorPolicy, policy => policy.RequireRole(StaticRoles.Admin, StaticRoles.Moderator));
                opt.AddPolicy(StaticPolicies.CompanyMembersPolicy, policy =>
                {
                    policy.AddRequirements(new CustomAuthorizations.CompanyMembers("findful.com"));
                });
            });

            services.AddSingleton<IAuthorizationHandler, CompanyMembersHandler>();

            return services;
        }

    }
}
