using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models.Entities.User;

namespace Core.IServices.User
{
    public interface ITokenService : IEntityService<RefreshToken>
    {
        Task<RefreshToken> GetByTokenAsync(string refreshToken);
    }
}
