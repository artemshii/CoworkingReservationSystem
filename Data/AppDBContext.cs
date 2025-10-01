using CoworkingReservation.Data.Models;
using CoworkingReservation.Data.Models.DbModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace CoworkingReservation.Data
{
    public class AppDBContext : DbContext
    {
        
        private IConfiguration _configuration { get; set; }
        public AppDBContext(DbContextOptions<AppDBContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<AuthUserData> AuthUsersData { get; set; }
        public DbSet<CoworkingUnit> CoworkingUnits { get; set; }
        public DbSet<CoworkingFlags> CoworkingFlags { get; set; }
        public DbSet<CoworkingPhoto> CoworkingPhotos { get; set; }
        public DbSet<Reservation> Reservations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CoworkingUnit>().HasMany(q => q.CoworkingPhotos).WithOne(p => p.CoworkingUnit).HasForeignKey(k => k.CoworkingUnitId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CoworkingUnit>().HasOne(q => q.CoworkingFlags).WithOne(p => p.CoworkingUnit).HasForeignKey<CoworkingFlags>(k => k.CoworkingUnitId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CoworkingUnit>().HasMany(q => q.Reservations).WithOne(p => p.CoworkingUnit).HasForeignKey(k => k.CoworkingUnitId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Reservation>().HasIndex(r => r.StartTime);

            modelBuilder.Entity<CoworkingUnit>().HasIndex(q => q.Name).IsUnique();




        }
    }
}
