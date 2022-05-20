using API.Services.Interfaces;
using Core.Models.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Helpers;

namespace API.Services.Classes
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<AppUser> _userManager;

        // SymmetricSecurityKey is type of encryption where there is ONLY ONE key to encryption and decryption.
        //  because in this situation (JWT) key remains on the server.
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration configuration, UserManager<AppUser> userManager)
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind(nameof(jwtSettings), jwtSettings);

            _userManager = userManager;
            _key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.SecretKey));
        } 
        public async Task<string> CreateToken(AppUser user)
        {

            // Claim: store some properties in out token about user and issued by server.
            var claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                //Used for Token Validation
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var roles = await _userManager.GetRolesAsync(user);

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var cred =  new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokeDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = cred
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokeDescriptor);

            return tokenHandler.WriteToken(token);

        }
    }
}
