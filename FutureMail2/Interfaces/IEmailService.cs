// IEmailService.cs
using System.Threading.Tasks;

public interface IEmailService
{
    Task<bool> SendEmailAsync(EmailModel email);
}