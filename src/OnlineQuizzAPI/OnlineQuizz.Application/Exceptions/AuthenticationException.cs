using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Application.Exceptions
{
    public class AuthenticationFailedException : DomainException
    {
        public AuthenticationFailedException()
            : base("Invalid email or password") { }
    }
}
