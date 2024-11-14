using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text.Json;
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

        public DbSet<BrokerCompany> BrokerCompanies { get; set; }

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

            List<BrokerCompany> brokerCompanies = LoadDataFromJson<BrokerCompany>("../TruckingSystem.Data/SeedData/BrokerCompanies.json");
            builder.Entity<BrokerCompany>().HasData(brokerCompanies);

            List<DriverManager> driverManagers = LoadDataFromJson<DriverManager>("../TruckingSystem.Data/SeedData/DriverManagers.json");
            builder.Entity<DriverManager>().HasData(driverManagers);

            List<Driver> drivers = LoadDataFromJson<Driver>("../TruckingSystem.Data/SeedData/Drivers.json");
            builder.Entity<Driver>().HasData(drivers);

            List<Load> loads = LoadDataFromJson<Load>("../TruckingSystem.Data/SeedData/Loads.json");
            builder.Entity<Load>().HasData(loads);

            List<Part> parts = LoadDataFromJson<Part>("../TruckingSystem.Data/SeedData/Parts.json");
            builder.Entity<Part>().HasData(parts);

            List<Trailer> trailers = LoadDataFromJson<Trailer>("../TruckingSystem.Data/SeedData/Trailers.json");
            builder.Entity<Trailer>().HasData(trailers);

            List<Truck> trucks = LoadDataFromJson<Truck>("../TruckingSystem.Data/SeedData/Trucks.json");
            builder.Entity<Truck>().HasData(trucks);
        }

        private List<T> LoadDataFromJson<T>(string path)
        {
            string jsonData = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<T>>(jsonData);
        }
    }
}





