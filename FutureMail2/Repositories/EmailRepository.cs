// EmailRepository.cs
using System.Collections.Generic;

public class EmailRepository : IEmailRepository
{
    private readonly EmailDbContext _context;

    public EmailRepository(EmailDbContext context)
    {
        _context = context;
    }

    public void SaveEmail(EmailModel email)
    {
        _context.Emails.Add(email);
        _context.SaveChanges();
    }

    public List<EmailModel> GetEmails()
    {
        // E-posta listeleme işlemleri burada yapılabilir
        return _context.Emails.ToList();
    }
}