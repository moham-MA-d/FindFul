using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static string GetUsername(this ClaimsPrincipal user)
        {
            //ClaimTypes.Name : Represent `JwtRegisteredClaimNames.UniqueName`
            //  that we've set inside our token => `TokenService`
            return user.FindFirst(ClaimTypes.Name)?.Value;
        }

        public static int GetUserId(this ClaimsPrincipal user)
        {
            //ClaimTypes.NameIdentifier : Represent `JwtRegisteredClaimNames.NameId`
            //  that we've set inside our token => `TokenService`
            return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }
    }
}
