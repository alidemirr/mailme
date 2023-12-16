// UserModel.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class UserModel
{
    [Key]
    public int UserId { get; set; }

    [Required]
    public string Email { get; set; }

    // Diğer kullanıcı bilgileri buraya eklenebilir

    public List<VerificationCode> VerificationCodes { get; set; }
}

// VerificationCode.cs
using System;
using System.ComponentModel.DataAnnotations;

public class VerificationCode
{
    [Key]
    public int VerificationCodeId { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public string Code { get; set; }

    [Required]
    public bool IsVerified { get; set; }

    [Required]
    public DateTime ExpiryDate { get; set; }
}

// IAuthenticationService.cs
using System.Threading.Tasks;

public interface IAuthenticationService
{
    Task<bool> RegisterUser(UserModel user);
    Task<string> GenerateVerificationCode(int userId);
    Task<bool> VerifyCode(int userId, string code);
}

// FutureEmailModel.cs
using System;
using System.ComponentModel.DataAnnotations;

public class FutureEmailModel
{
    [Key]
    public int FutureEmailId { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public string Subject { get; set; }

    [Required]
    public string Body { get; set; }

    [Required]
    public DateTime ScheduledDate { get; set; }

    [Required]
    public bool IsSent { get; set; }
}

// IFutureEmailService.cs
using System.Threading.Tasks;

public interface IFutureEmailService
{
    Task<bool> ScheduleEmail(FutureEmailModel email);
    Task<bool> SendScheduledEmails();
}

// EmailModel.cs
using System;
using System.ComponentModel.DataAnnotations;

public class EmailModel
{
    [Key]
    public int EmailId { get; set; }
    
    [Required]
    public string Subject { get; set; }
    
    [Required]
    public string Body { get; set; }
    
    [Required]
    public DateTime SendDate { get; set; }
    
    [Required]
    public string Recipient { get; set; }
}

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

// EmailRepository.cs
using System.Collections.Generic;
using System.Linq;

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
        return _context.Emails.ToList();
    }
}

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

// EmailDbContext.cs
using Microsoft.EntityFrameworkCore;

public class EmailDbContext : DbContext
{
    public EmailDbContext(DbContextOptions<EmailDbContext> options)
        : base(options)
    {
    }

    public DbSet<EmailModel> Emails { get; set; }

    // Diğer DbSet'ler ve gerekli tablolar burada tanımlanabilir

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Fluent API veya diğer konfigürasyonlar burada tanımlanabilir
    }
}

// IEmailRepository.cs
using System.Collections.Generic;

public interface IEmailRepository
{
    void SaveEmail(EmailModel email);
    List<EmailModel> GetEmails();
    // Diğer e-posta ile ilgili veritabanı işlemleri için metodlar...
}

// IEmailService.cs
using System.Threading.Tasks;

public interface IEmailService
{
    Task<bool> SendEmailAsync(EmailModel email);
}

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
// Diğer gerekli using ifadeleri

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // Veritabanı bağlantısını eklemek için gerekli servisler burada eklenecek
        services.AddDbContext<EmailDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IEmailRepository, EmailRepository>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<EmailValidation>();

        // Diğer servislerin eklenmesi
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }

}
