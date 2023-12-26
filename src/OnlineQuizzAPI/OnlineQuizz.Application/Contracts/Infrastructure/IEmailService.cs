using OnlineQuizz.Application.Models.Mail;
using System.Threading.Tasks;

namespace OnlineQuizz.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
    }
}
