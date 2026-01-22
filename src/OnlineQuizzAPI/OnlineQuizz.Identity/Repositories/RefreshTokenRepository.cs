using Microsoft.EntityFrameworkCore;
using OnlineQuizz.Identity.Contracts;
using OnlineQuizz.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Identity.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly OnlineQuizzIdentityDbContext _context;

        public RefreshTokenRepository(OnlineQuizzIdentityDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();
        }

        public async Task<RefreshToken?> GetAsync(string hashedToken)
        {
            return await _context.RefreshTokens
                .Include(rt => rt.User)
                .FirstOrDefaultAsync(x => x.HashedToken == hashedToken);
        }

        public async Task<RefreshToken?> GetUserActiveRefreshTokenAsync(string userId)
        {
            return await _context.RefreshTokens
                .OrderByDescending(x => x.CreatedDate)
                .FirstOrDefaultAsync(x =>
                    x.UserId == userId &&
                    x.Expires > DateTime.UtcNow &&
                    !x.IsRevoked);
        }


        public async Task RevokeAsync(RefreshToken refreshToken)
        {
            refreshToken.IsRevoked = true;
            refreshToken.Expires = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        public async Task RotateAsync(RefreshToken existingToken, RefreshToken newToken)
        {
            _context.RefreshTokens.Add(newToken);
            existingToken.IsRevoked = true;
            existingToken.Expires = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        public async Task RevokeAllAsync(string userId)
        {
            var tokens = await _context.RefreshTokens
                .Where(x => x.UserId == userId && !x.IsRevoked)
                .ToListAsync();

            foreach (var token in tokens)
            {
                token.IsRevoked = true;
                token.Expires = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
        }
    }

}
