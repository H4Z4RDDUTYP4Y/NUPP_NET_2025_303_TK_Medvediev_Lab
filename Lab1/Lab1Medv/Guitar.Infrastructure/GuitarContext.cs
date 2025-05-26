using Guitar.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Guitar.Infrastructure
{
    public class GuitarContext : DbContext
    {
        public GuitarContext(DbContextOptions<GuitarContext> options)
            : base(options)
        {
        }

        // DbSet для кожної моделі
        public DbSet<GuitarModel> Guitars { get; set; }
        public DbSet<ElectricModel> Electrics { get; set; }
        public DbSet<AcousticModel> Acoustics { get; set; }
        public DbSet<PlayerModel> Players { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Наслідування через Discriminator
            modelBuilder.Entity<GuitarModel>()
                        .HasDiscriminator<string>("GuitarType")
                        .HasValue<ElectricModel>("Electric")
                        .HasValue<AcousticModel>("Acoustic");

            base.OnModelCreating(modelBuilder);
        }
    }
}
