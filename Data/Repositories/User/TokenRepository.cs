using System;
using System.Linq;
using System.Threading.Tasks;
using Core.IRepositories.User;
using Core.Models.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data.Repositories.User
{
    public class TokenRepository : GenericRepository<RefreshToken>, ITokenRepository
    {
        public TokenRepository(DataContext context, ILogger logger) : base(context, logger)
        {
        }

        public async Task<RefreshToken> GetByTokenAsync(string refreshToken)
        {
            return await _context.RefreshTokens.SingleOrDefaultAsync(x => x.Token == refreshToken);
        }
    }
}
