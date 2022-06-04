using System;
using System.Threading.Tasks;
using AutoMapper;
using DTO.Account;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using SDK.Tester.Classes;
using SDK.Tester.Interfaces;

namespace SDK.Tester
{
    class Program
    {

        static async Task Main(string[] args)
        {

            var cachedToken = string.Empty;
            var identityApi = RestService.For<IIdentityApi>("https://localhost:44341");

            //var registerResponse = await identityApi.RegisterAsync(new DtoRegister
            //{
            //    Email = "mohammad1@gamil.com",
            //    Password = "Pa$$w0rd",
            //    UserName = "mohammad1"
            //});

            var loginResponse = await identityApi.LoginAsync(new DtoLogin
            {
                UserName = "mohammad1",
                Password = "Pa$$w0rd"
            });

            if (loginResponse.Content != null) cachedToken = loginResponse.Content.Token;


            var services = new Microsoft.Extensions.DependencyInjection.ServiceCollection();

            var startup = new Startup();
            startup.ConfigureServices(services, cachedToken);

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            // entry to run app
            serviceProvider.GetRequiredService<Runner>()?.Run();
            //Generate the implementation for this API
            
        }
    }


    public class Startup
    {
        public void ConfigureServices(IServiceCollection services, string cachedToken)
        {
            services.AddScoped<Runner>();
            services.AddScoped<IPostSdk>(x => new PostSdk(cachedToken));
            services.AddScoped<IPostApi>(x => RestService.For<IPostApi>("https://localhost:44341", new RefitSettings
            {
                //it release a delegate that return a string that put in authorization header.
                AuthorizationHeaderValueGetter = () => Task.FromResult(cachedToken)
            }));
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

        }
    }
    public class Runner
    {
        private readonly IPostSdk _postSdk;
        private readonly IMapper _mapper;

        public Runner(IPostSdk postSdk, IMapper mapper)
        {
            _postSdk = postSdk;
            _mapper = mapper;
        }

        public async Task Run()
        {
            // I can use the point calculator!

            var addedPost = await _postSdk.AddAsync();
            var allPosts = await _postSdk.GetAllAsync();
            if (addedPost.Content != null)
            {
                var postResponse = await _postSdk.GetAsync(11);
                var postRequest = _postSdk.GetPostRequestFromPostResponse(postResponse.Content);
                var updatedPost = await _postSdk.EditAsync(postRequest);
            }

            var deletedPostResult = await _postSdk.DeleteAsync(1);
        }
    }

}
