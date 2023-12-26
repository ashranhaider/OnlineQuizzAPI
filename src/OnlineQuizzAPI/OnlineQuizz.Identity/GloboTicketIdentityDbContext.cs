using OnlineQuizz.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OnlineQuizz.Identity
{
    public class OnlineQuizzIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public OnlineQuizzIdentityDbContext()
        {

        }

        public OnlineQuizzIdentityDbContext(DbContextOptions<OnlineQuizzIdentityDbContext> options) : base(options)
        {
        }
    }
}
