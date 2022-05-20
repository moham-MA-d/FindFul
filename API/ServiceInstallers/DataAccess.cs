using API.Services.Classes;
using API.Services.Interfaces;
using Core.IRepositories.Follows;
using Core.IRepositories.Messages;
using Core.IRepositories.Posts;
using Core.IRepositories.User;
using Core.IServices.Follows;
using Core.IServices.Mapper;
using Core.IServices.Messages;
using Core.IServices.Posts;
using Core.IServices.User;
using Data.Repositories.Follows;
using Data.Repositories.Messages;
using Data.Repositories.Posts;
using Data.Repositories.User;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.Classes.Follows;
using Service.Classes.Mapper;
using Service.Classes.Messages;
using Service.Classes.Posts;
using Service.Classes.User;

namespace API.ServiceInstallers
{
    public class DataAccess : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMapperService, MapperService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IPostService, PostService>();

            services.AddScoped<IUserPhotoRepository, UserPhotoRepository>();
            services.AddScoped<IUserPhotoService, UserPhotoService>();

            services.AddScoped<IFollowRepository, FollowRepository>();
            services.AddScoped<IFollowService, FollowService>();

            services.AddScoped<IPostLikedRepository, PostLikedRepository>();
            services.AddScoped<IPostedLikedService, PostLikedService>();

            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IMessageService, MessageService>();

            services.AddScoped<IPhotoServiceAPI, PhotoServiceAPI>();
        }
    }
}
