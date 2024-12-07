using Microsoft.EntityFrameworkCore;
using web_programlama.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace web_programlama.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Salon> Salonlar { get; set; }
        public DbSet<Calisan> Calisanlar { get; set; }
        public DbSet<Islem> Islemler { get; set; }
        public DbSet<Randevu> Randevular { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
