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
