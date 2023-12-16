// IEmailRepository.cs
using System.Collections.Generic;

public interface IEmailRepository
{
    void SaveEmail(EmailModel email);
    List<EmailModel> GetEmails();
    // Diğer e-posta ile ilgili veritabanı işlemleri için metodlar...
}