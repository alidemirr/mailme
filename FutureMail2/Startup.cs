// Startup.cs
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // Veritabanı bağlantısını eklemek için gerekli servisler burada eklenecek
        services.AddDbContext<EmailDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IEmailRepository, EmailRepository>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<EmailValidation>(); // EmailValidation'ı da ekledik.
        // Diğer servislerin eklenmesi
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configure metodu burada güncellenmeli
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            // Prodüksiyon ortamı için gerekli ayarlar
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