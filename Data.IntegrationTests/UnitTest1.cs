using System;
using System.Net.Http;
using System.Threading.Tasks;
using API;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Data.IntegrationTests
{
    public class UnitTest1
    {
        //_clientApi knows how to target in-memory version of API project
        private readonly HttpClient _clientApi;

        public UnitTest1()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            _clientApi = appFactory.CreateClient();
        }

        [Fact]
        public async Task Test1()
        {
            string username = "chambers";
            var a = await _clientApi.GetAsync($"api/v1/Users/GetUser/{username}");
        }
    }
}
