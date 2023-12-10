using API.Services.Interfaces;
using DTO.SocialNetworks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace API.Services.Classes
{
    public class FacebookService : IFacebookService
    {
        //https://graph.facebook.com/debug_token?input_token=EAAHsi0t6pJ0BAAZBk4ITGOvTR7ZBK9zD0rn84xKcah4XS3txQiamjSMWgwnuQu2QC1A5ft2teTp242MKgiuRZCuUm1dC38sjkrwSssXgPZA0B8Huo3RXwFPiO4TVDsnZCuHR6RdiSTKl1pJyaZCzl7hmULldyHOAStZA3tjAu1mHyscQcjcfM3brnsAuQi1XtMZAuvBkbMu7eJMNyq6WYmhM&access_token=|
        private const string TokenValidationUrl = "https://graph.facebook.com/debug_token?input_token={0}&access_token={1}|{2}";
        private const string UserInfoUrl = "https://graph.facebook.com/me?fields=first_name,last_name,picture,email&access_token={0}";
        
        private readonly FacebookAuthSetting  _facebookAuthSetting;
        
        //Used to create http client for this service
        private readonly IHttpClientFactory _httpClientFactory;

        public FacebookService(IHttpClientFactory httpClientFactory, FacebookAuthSetting facebookAuthSetting)
        {
            _httpClientFactory = httpClientFactory;
            _facebookAuthSetting = facebookAuthSetting;

        }


        public async Task<FacebookUserInfo> GetUserInfoAsync(string accessToken)
        {
            var formattedUrl = string.Format(UserInfoUrl, accessToken, _facebookAuthSetting.AppId, _facebookAuthSetting.AppSecret);

            var result = await _httpClientFactory.CreateClient().GetAsync(formattedUrl);

            result.EnsureSuccessStatusCode();

            var stringResponse = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<FacebookUserInfo>(stringResponse);
        }

        public async Task<FacebookTokenValidation> ValidateAccessTokenAsync(string accessToken)
        {
            var formattedUrl = string.Format(TokenValidationUrl, accessToken, _facebookAuthSetting.AppId, _facebookAuthSetting.AppSecret);

            var result = await _httpClientFactory.CreateClient().GetAsync(formattedUrl);

            result.EnsureSuccessStatusCode();

            var stringResponse = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<FacebookTokenValidation>(stringResponse);
        }
    }
}
