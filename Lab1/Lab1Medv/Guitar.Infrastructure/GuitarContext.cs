using Guitar.Infrastructure.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Guitar.Infrastructure; // Для ApplicationUser



namespace Guitar.Infrastructure
{
    public class GuitarContext : IdentityDbContext<ApplicationUser>
    {
        public GuitarContext(DbContextOptions<GuitarContext> options)
            : base(options)
        {
        }

        public DbSet<GuitarModel> Guitars { get; set; }
        public DbSet<ElectricModel> Electrics { get; set; }
        public DbSet<AcousticModel> Acoustics { get; set; }
        public DbSet<PlayerModel> Players { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GuitarModel>()
                        .HasDiscriminator<string>("GuitarType")
                        .HasValue<ElectricModel>("Electric")
                        .HasValue<AcousticModel>("Acoustic");
        }
    }
}