using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Application.Features.Auth.Register.Commands
{
    public class RegistrationResponse
    {
        public string UserId { get; set; } = String.Empty;
        public string Message { get; set; } = String.Empty;
    }
}
