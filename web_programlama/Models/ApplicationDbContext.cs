using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace web_programlama.Models
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Mevcut veritabanı tabloları
        public DbSet<Salon> Salonlar { get; set; } = null!;
        public DbSet<Calisan> Calisanlar { get; set; } = null!;
        public DbSet<Islem> Islemler { get; set; } = null!;
        public DbSet<Randevu> Randevular { get; set; } = null!;
    }
}
