using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TruckingSystem.Data.Models;

namespace TruckingSystem.Data
{
    public class TruckingSystemDbContext : IdentityDbContext
    {
        public TruckingSystemDbContext()
        {   
        }
        public TruckingSystemDbContext(DbContextOptions<TruckingSystemDbContext> options)
            : base(options)
        {
        }

        public DbSet<AvailableDispatch> AvailableDispatches { get; set; }

        public DbSet<BrokerCompany> BrokerCompanies { get; set; }

        public DbSet<CompletedDispatch> CompletedDispatches { get; set; }

        public DbSet<Dispatch> Dispatches { get; set; }

        public DbSet<Driver> Drivers { get; set; }

        public DbSet<DriverManager> DriverManagers { get; set; }

        public DbSet<Load> Loads { get; set; }

        public DbSet<Part> Parts { get; set; }

        public DbSet<Trailer> Trailers { get; set; }

        public DbSet<Truck> Trucks { get; set; }

        public DbSet<TruckPart> TrucksParts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
