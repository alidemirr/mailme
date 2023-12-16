// EmailValidation.cs
public class EmailValidation
{
    public bool IsValidEmail(EmailModel email)
    {
        // E-posta doğrulama kurallarını burada uygulayabilirsiniz
        // Örneğin: gerekli alanların doldurulup doldurulmadığını, geçerli bir tarih seçildiğini kontrol edebilirsiniz
        if (string.IsNullOrWhiteSpace(email.Body) || string.IsNullOrWhiteSpace(email.Recipient))
        {
            return false;
        }
        return true;
    }
}