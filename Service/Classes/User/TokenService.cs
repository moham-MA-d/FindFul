using System;
using System.Threading.Tasks;
using Core;
using Core.IRepositories;
using Core.IRepositories.User;
using Core.IServices.User;
using Core.Models.Entities.User;

namespace Service.Classes.User
{
    public class TokenService : EntityService<RefreshToken>, ITokenService
    {
        private readonly ITokenRepository _tokenRepository;

        public TokenService(IUnitOfWork unitOfWork, ITokenRepository tokenRepository) : base(unitOfWork, tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }

        public async Task<RefreshToken> GetByTokenAsync(string refreshToken)
        {
            return await _tokenRepository.GetByTokenAsync(refreshToken);
        }
    }
}
