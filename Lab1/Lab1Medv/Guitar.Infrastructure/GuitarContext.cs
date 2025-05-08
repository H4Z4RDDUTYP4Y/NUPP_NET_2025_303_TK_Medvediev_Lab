using Guitar.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Guitar.Infrastructure
{
    public class GuitarContext : DbContext
    {
        public GuitarContext(DbContextOptions<GuitarContext> options) : base(options) { }

        public DbSet<GuitarModel> Guitars => Set<GuitarModel>();
        public DbSet<ElectricModel> Electrics => Set<ElectricModel>();
        public DbSet<AcousticModel> Acoustics => Set<AcousticModel>();
        public DbSet<PlayerModel> Players => Set<PlayerModel>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GuitarModel>()
                        .HasDiscriminator<string>("GuitarType")
                        .HasValue<ElectricModel>("Electric")
                        .HasValue<AcousticModel>("Acoustic");

            base.OnModelCreating(modelBuilder);
        }
    }
}
