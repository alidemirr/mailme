// EmailService.cs
using System;
using System.Threading.Tasks;

public class EmailService : IEmailService
{
    public async Task<bool> SendEmailAsync(EmailModel email)
    {
        try
        {
            // E-posta gönderme işlemleri burada gerçekleştirilebilir
            // Örneğin: bir SMTP sunucusuna bağlanma, e-posta gönderme işlemi gibi adımlar olabilir

            // Burada gönderim gerçekleştiği varsayalım
            Console.WriteLine("E-posta başarıyla gönderildi.");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"E-posta gönderilirken hata oluştu: {ex.Message}");
            return false;
        }
    }
}
