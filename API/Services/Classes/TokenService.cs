using API.Services.Interfaces;
using DTO.Account;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Services.Classes
{
    public class TokenService : ITokenService
    {
        // SymmetricSecurityKey is type of encryption where there is ONLY ONE key to encryption and decryption.
        //  because in this situation (JWT) key remains on the server.
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration configuration)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"]));
        }
        public string CreateToken(string username, int userId)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, username)
            };

            var creds =  new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokeDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokeDescriptor);

            return tokenHandler.WriteToken(token);


            throw new System.NotImplementedException();
        }
    }
}
