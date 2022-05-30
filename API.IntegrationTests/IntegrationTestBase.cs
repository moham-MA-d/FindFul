using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Data;
using DTO.Account;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace API.IntegrationTests
{
    public class IntegrationTestBase
    {
        protected readonly HttpClient _httpClient;

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

            _httpClient = appFactory.CreateClient();
        }

        protected async Task AuthenticateAsync()
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("bearer", await GetJwtAsync());
        }

        private async Task<string> GetJwtAsync()
        {
            var response = await _httpClient.PostAsJsonAsync($"api/V1/Account/Register", new DtoRegister
            {
                Email = "test2@integration.com",
                Password = "Pa$$w0rd",
                UserName = "test2Integration"
            });

            //var registrationResponseString = await response.Content.ReadAsStringAsync();
            //var registrationResponse = JsonSerializer.Deserialize<DtoAuthenticationResult>(registrationResponseString,
            //    new JsonSerializerOptions
            //    {
            //        PropertyNameCaseInsensitive = true
            //    });
            //return registrationResponse?.Token;


            var registrationResponseString = await response.Content.ReadFromJsonAsync<DtoAuthenticationResult>();

            return registrationResponseString?.Token;
        }
    }
}
