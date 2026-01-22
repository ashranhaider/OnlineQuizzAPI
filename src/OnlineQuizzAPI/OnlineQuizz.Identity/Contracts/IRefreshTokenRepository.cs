using OnlineQuizz.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Identity.Contracts
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(RefreshToken refreshToken);
        Task<RefreshToken?> GetAsync(string token);
        Task<RefreshToken?> GetUserActiveRefreshTokenAsync(string userId);
        Task RevokeAsync(RefreshToken refreshToken);
        Task RotateAsync(RefreshToken existingToken, RefreshToken newToken);
        Task RevokeAllAsync(string userId);
    }

}
