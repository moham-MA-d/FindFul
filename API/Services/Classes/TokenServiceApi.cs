using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Helpers;
using API.Services.Interfaces;
using Core.IServices.User;
using Core.Models.Entities.User;
using DTO.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Services.Classes
{
    public class TokenServiceApi : ITokenServiceApi
    {
        private readonly JwtSettings _jwtSettings;
        private readonly ITokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;
        private readonly TokenValidationParameters _validationParameters;

        // SymmetricSecurityKey is type of encryption where there is ONLY ONE key to encryption and decryption.
        //  because in this situation (JWT) key remains on the server.
        private readonly SymmetricSecurityKey _key;

        public TokenServiceApi(IConfiguration configuration, UserManager<AppUser> userManager, TokenValidationParameters validationParameters, JwtSettings jwtSettings, ITokenService tokenService)
        {
            _validationParameters = validationParameters;
            _jwtSettings = jwtSettings;
            _tokenService = tokenService;
            configuration.Bind(nameof(_jwtSettings), _jwtSettings);

            _userManager = userManager;
            _key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.SecretKey));
        }


        public async Task<DtoAuthenticationResult> RefreshTokenAsync(string token, string refreshToken) 
        {
            var validatedToken = GetClaimsPrincipalFromToken(token);
            if (validatedToken == null)
                return new DtoAuthenticationResult { Errors = new[] { "Invalid RefreshToken!" } };

            var claimType = validatedToken.Claims.SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp)?.Value;

            long expiryDateUnix = 0;
            if (claimType != null)
                expiryDateUnix = long.Parse(claimType);

            var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            expiryDateTimeUtc = expiryDateTimeUtc.AddSeconds(expiryDateUnix).ToUniversalTime();


            if (expiryDateTimeUtc > DateTime.UtcNow)
                return new DtoAuthenticationResult { Errors = new[] { "This Token has not expired yet!" } };

            // tokenId
            var jti = validatedToken.Claims.SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti)?.Value;

            if (jti == null)
                return new DtoAuthenticationResult { Errors = new[] { "Try to login!" } };

            var storedRefreshToken = await _tokenService.GetByTokenAsync(refreshToken);

            if (DateTime.UtcNow > storedRefreshToken.ExpireDateTime)
                return new DtoAuthenticationResult { Errors = new[] { "This Token has expired!" } };

            if (storedRefreshToken.IsInvalidated)
                return new DtoAuthenticationResult { Errors = new[] { "This Token has been invalidated!" } };

            if (storedRefreshToken.IsUsed)
                return new DtoAuthenticationResult { Errors = new[] { "This Token has been used!" } };

            if (storedRefreshToken.JwtId != jti)
                return new DtoAuthenticationResult { Errors = new[] { "Your Token is manipulated" } };

            storedRefreshToken.IsUsed = true;
            _tokenService.Update(storedRefreshToken);

            var userId = validatedToken.Claims.SingleOrDefault(x => x.Type == "Id")?.Value;
            if (string.IsNullOrEmpty(userId))
                return new DtoAuthenticationResult { Errors = new[] { "Please try again later" } }; //User was not found

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == Int32.Parse(userId));
            
            return await CreateTokenAsync(user);
        }
        
        public async Task<DtoAuthenticationResult> CreateTokenAsync(AppUser user)
        {
            // Claim: store some properties in out token about user and issued by server.
            var claims = new List<Claim>
            {
                new ("Email", user.Email),
                new ("Id", user.Id.ToString()),
                new ("UserName", user.UserName),
                new ("Sex", user.Sex.ToString()),
                new ("Gender", user.Gender.ToString()),
                new ("PhotoUrl", user.ProfilePhotoUrl ?? "/assets/images/user.png"),
                //new Claim(JwtRegisteredClaimNames.Exp, DateTime.UtcNow.AddMonths(1).ToString(CultureInfo.InvariantCulture)),
                //Used for RefreshToken Validation
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var roles = await _userManager.GetRolesAsync(user);

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var cred = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                //Expires = DateTime.Now.Add(_jwtSettings.TokenLifeTime),
                Expires = DateTime.UtcNow.AddSeconds(120),
                SigningCredentials = cred
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            var refreshToken = new RefreshToken
            {
                JwtId = token.Id,
                UserId = user.Id,
                CreateDateTime = DateTime.UtcNow,
                ExpireDateTime = DateTime.UtcNow.AddMonths(6)
            };
            await _tokenService.AddAsync(refreshToken);

            return new DtoAuthenticationResult
            {
                Success = true,
                RefreshToken = refreshToken.Token,
                Token = tokenHandler.WriteToken(token)
            };

        }

        public ClaimsPrincipal GetClaimsPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                //var principal =  await tokenHandler.ValidateTokenAsync(token, _validationParameters);
                var principal = tokenHandler.ValidateToken(token, _validationParameters, out var validatedToken);
                return !HasJwtValidSecurityAlgorithm(validatedToken) ? null : principal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        private static bool HasJwtValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return validatedToken is JwtSecurityToken jwtSecurityToken
                   && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
