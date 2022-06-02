using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Core.Models.Entities.User;
using Data;
using DTO.Account;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace API.IntegrationTests
{
    public class IntegrationTestBase/*<TStartup> : WebApplicationFactory<TStartup> where TStartup : class*/ : IDisposable
    {
        protected readonly HttpClient _httpClient;
        protected  DtoRegisterId _dtoRegisterId;
        protected  DtoMember _dtoMember;
        private readonly IServiceProvider _serviceProvider;
        protected IntegrationTestBase()
        {
            //Because we are using EF we can change our SQL server database with an in-memory one.
            var appFactory = new WebApplicationFactory<Startup>()
                    .WithWebHostBuilder(builder =>
                    {
                        // at this point Startup execution has been finished.
                        builder.ConfigureServices(services =>
                        {
                            //DataContext is using SQL server structure.
                            services.RemoveAll(typeof(DbContextOptions<DataContext>));

                            services.AddDbContext<DataContext>(options =>
                            {
                                options.UseInMemoryDatabase("FindFulInMemoryDb");
                            });
                        });
                    });

            _serviceProvider = appFactory.Services;
            _httpClient = appFactory.CreateClient();

            using var serviceScope = _serviceProvider.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<DataContext>();

            //var logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<IntegrationTestBase<TStartup>>>();

            //context.Database.EnsureDeleted();

            try
            {
                Console.WriteLine("Seeding...");
                var roles = new List<AppRole>()
                {
                    new AppRole {Name = "Admin", NormalizedName = "ADMIN"},
                    new AppRole {Name = "Moderator", NormalizedName = "MODERATOR"},
                    new AppRole {Name = "DtoMember", NormalizedName = "MEMBER"}
                };
                context.Roles.AddRange(roles);
                context.SaveChanges();

            }
            catch (Exception ex)
            {
               // logger.LogError(ex, "An error occurred seeding the " + "database with test messages. Error: {Message}", ex.Message);
            }

            //context.Database.EnsureCreated();

        }

        public void Dispose()
        {
            //Dispose(disposing: true);
            //GC.SuppressFinalize(this);
            using var serviceScope = _serviceProvider.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<DataContext>();
            context.Database.EnsureDeleted();
        }

        protected async Task AuthenticateAsync()
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("bearer", await GetJwtAsync());
        }

        private async Task<string> GetJwtAsync()
        {
            var response = await _httpClient.PostAsJsonAsync($"api/V1/Account/Register", CreateUserObject());

            var registrationResponseString = await response.Content.ReadFromJsonAsync<DtoAuthenticationResult>();

            return registrationResponseString?.Token;
        }

        public class DtoRegisterId : DtoRegister
        {
            public int Id { get; set; }
        }
        public DtoRegisterId CreateUserObject()
        {
            _dtoRegisterId = new DtoRegisterId
            {
                Id = 1,
                Email = "test@integration.com",
                Password = "Pa$$w0rd",
                UserName = "testIntegration"
            };
            return _dtoRegisterId;
        }

        public DtoMember MapDtoMember(DtoRegisterId dtoRegisterId)
        {
            _dtoMember = new DtoMember()
            {
                Id = dtoRegisterId.Id,
                Email = dtoRegisterId.Email,
                UserName = dtoRegisterId.UserName
            };
            return _dtoMember;
        }

    }
}
