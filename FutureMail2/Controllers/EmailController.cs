// EmailController.cs
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

public class EmailController : Controller
{
    private readonly IEmailService _emailService;
    private readonly EmailValidation _emailValidation;

    public EmailController(IEmailService emailService, EmailValidation emailValidation)
    {
        _emailService = emailService;
        _emailValidation = emailValidation;
    }

    // E-posta gönderme sayfasını gösteren action
    public IActionResult SendEmail()
    {
        return View(); // Gönderim formunun olduğu view'i göster
    }

    // Gönder butonuna basıldığında çalışacak action
    [HttpPost]
    public async Task<IActionResult> SendEmail(EmailModel email)
    {
        if (!_emailValidation.IsValidEmail(email))
        {
            ModelState.AddModelError(string.Empty, "Geçersiz e-posta bilgileri."); // Doğrulama hatası
            return View(email);
        }

        var result = await _emailService.SendEmailAsync(email);
        if (result)
        {
            ViewBag.SuccessMessage = "E-posta başarıyla gönderildi."; // Gönderim başarılı
        }
        else
        {
            ViewBag.ErrorMessage = "E-posta gönderilirken bir hata oluştu."; // Gönderim hatası
        }

        return View();
    }
}