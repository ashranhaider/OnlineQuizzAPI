using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Identity.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string HashedToken { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public DateTime Expires { get; set; }
        public bool IsRevoked { get; set; }
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
    }

}
