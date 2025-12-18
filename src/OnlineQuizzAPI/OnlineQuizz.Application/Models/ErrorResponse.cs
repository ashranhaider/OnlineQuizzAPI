using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Application.Models
{
    public class ErrorResponse
    {
        public string Code { get; set; } = default!;
        public string Message { get; set; } = default!;
        public object? Errors { get; set; }
    }
}
