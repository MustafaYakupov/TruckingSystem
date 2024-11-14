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

            List<BrokerCompany> brokerCompanies = LoadDataFromJson("../TruckingSystem.Data/SeedData/BrokerCompanies.json");
            builder.Entity<BrokerCompany>().HasData(brokerCompanies);

            //List<Guid> brokerCompanyIds = GenerateBrokerCompanyIds();

            //builder.Entity<Part>()
            //    .HasData(this.GenerateParts());

            //builder.Entity<Trailer>()
            //    .HasData(this.GenerateTrailers());

            //builder.Entity<Truck>()
            //    .HasData(this.GenerateTrucks());

            //builder.Entity<DriverManager>()
            //    .HasData(this.GenerateDriverManagers());

            //builder.Entity<Driver>()
            //    .HasData(this.GenerateDrivers());

            //builder.Entity<BrokerCompany>()
            //    .HasData(this.GenerateBrokerCompanies(brokerCompanyIds));

            //builder.Entity<Load>()
            //    .HasData(this.GenerateLoads(brokerCompanyIds));

            //builder.Entity<Dispatch>()
            //    .HasData(this.GenerateDispatches());
        }

        private List<BrokerCompany> LoadDataFromJson(string path)
        {
            var jsonData = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<BrokerCompany>>(jsonData);
        }

        //private List<Guid> GenerateBrokerCompanyIds()
        //{
        //    Guid brokerCompanyId1 = Guid.NewGuid();
        //    Guid brokerCompanyId2 = Guid.NewGuid();
        //    Guid brokerCompanyId3 = Guid.NewGuid();

        //    List<Guid> brokerCompanyIds = new List<Guid>()
        //    {
        //        brokerCompanyId1,
        //        brokerCompanyId2,
        //        brokerCompanyId3
        //    };

        //    return brokerCompanyIds;
        //}

        //private IEnumerable<Part> GenerateParts()
        //{
        //    IEnumerable<Part> parts = new List<Part>()
        //    {
        //        new Part()
        //        {
        //            Type = "Tire",
        //            Make = "Michelin",
        //        }
        //    };

        //    return parts;
        //}

        //private IEnumerable<Trailer> GenerateTrailers()
        //{
        //    IEnumerable<Trailer> trailers = new List<Trailer>()
        //    {
        //        new Trailer()
        //        {
        //            Make = "Thermo King",
        //            Type = "Reefer",
        //            TrailerNumber = 540,
        //            ModelYear = 2020,
        //        }
        //    };

        //    return trailers;
        //}

        //private IEnumerable<Truck> GenerateTrucks()
        //{
        //    IEnumerable<Truck> trucks = new List<Truck>()
        //    {
        //        new Truck()
        //        {
        //            TruckNumber = 2140,
        //            Color = "White",
        //            LicensePlate = "LT5500",
        //            Make = "Freightliner",
        //            Model = "Cascadia",
        //            ModelYear = 2020,
        //        }
        //    };

        //    return trucks;
        //}

        //private IEnumerable<DriverManager> GenerateDriverManagers()
        //{
        //    IEnumerable<DriverManager> driverManagers = new List<DriverManager>()
        //    {
        //        new DriverManager()
        //        {
        //            FirstName = "John",
        //            LastName = "Peterson",
        //        }
        //    };

        //    return driverManagers;
        //}

        //private IEnumerable<Driver> GenerateDrivers()
        //{

        //    IEnumerable<Driver> drivers = new List<Driver>()
        //    {
        //        new Driver()
        //        {
        //            FirstName = "Sergio",
        //            LastName = "Hernandez",
        //            LicenseNumber = "8000AS50"
        //        }
        //    };

        //    return drivers;
        //}

        //private IEnumerable<BrokerCompany> GenerateBrokerCompanies(List<Guid> brokerCompanyIds)
        //{
        //    IEnumerable<BrokerCompany> companies = new List<BrokerCompany>()
        //    {
        //        new BrokerCompany()
        //        {
        //            Id = brokerCompanyIds[0],
        //            CompanyName = "C.H. Robinson"
        //        },
        //        new BrokerCompany()
        //        {
        //            Id = brokerCompanyIds[1],
        //            CompanyName = "Coyote"
        //        },
        //        new BrokerCompany()
        //        {
        //            Id = brokerCompanyIds[2],
        //            CompanyName = "J.B. Hunt"
        //        }
        //    };

        //    return companies;
        //}

        //private IEnumerable<Load> GenerateLoads(List<Guid> brokerCompanyIds)
        //{
        //    IEnumerable<Load> loads = new List<Load>()
        //    {
        //        new Load()
        //        {
        //            PickupLocation = "New York, WA",
        //            DeliveryLocation = "San Diego, CA",
        //            Weight = 40000,
        //            PickupTime = new DateTime(2024, 2, 15),
        //            DeliveryTime = DateTime.Now,
        //            Distance = 590,
        //            BrokerCompanyId = brokerCompanyIds[0],
        //        },

        //        new Load()
        //        {
        //            PickupLocation = "New York, WA",
        //            DeliveryLocation = "San Diego, CA",
        //            Weight = 40000,
        //            PickupTime = new DateTime(2024, 2, 15),
        //            DeliveryTime = DateTime.Now,
        //            Distance = 590,
        //            BrokerCompanyId = brokerCompanyIds[0],
        //        },
        //    };

        //    return loads;
        //}

        //private IEnumerable<Dispatch> GenerateDispatches()
        //{
        //    DateTime pickupTime;
        //    DateTime deliveryTime;

        //    if (DateTime.TryParseExact("12-02-2024", "MM-dd-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out pickupTime))
        //    {
        //    }

        //    if (DateTime.TryParseExact("12-05-2024", "MM-dd-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out deliveryTime))
        //    {
        //    }

        //    IEnumerable<Dispatch> dispatches = new List<Dispatch>()
        //    {
        //        new Dispatch()
        //        {
        //            Driver = new Driver()
        //            {
        //                FirstName = "John",
        //                LastName = "Wayne",
        //                LicenseNumber = "9000SWW5",
        //            },
        //            Truck = new Truck()
        //            {
        //                TruckNumber = 2250,
        //                Color = "White",
        //                LicensePlate = "SR500",
        //                Make = "International",
        //                Model = "LT500",
        //                ModelYear = 2023,
        //            },
        //            Trailer = new Trailer()
        //            {
        //                Make = "Carrier",
        //                Type = "Reefer",
        //                TrailerNumber = 600,
        //                ModelYear = 2021,
        //            },
        //            DriverManager = new DriverManager()
        //            {
        //                FirstName = "John",
        //                LastName = "Peterson",
        //            },
        //            Load = new Load()
        //            {
        //                PickupLocation = "New York, WA",
        //                DeliveryLocation = "San Diego, CA",
        //                Weight = 40000,
        //                PickupTime = pickupTime,
        //                DeliveryTime = deliveryTime,
        //                Distance = 590,
        //                BrokerCompany = new BrokerCompany()
        //                {
        //                    CompanyName = "Tucker",
        //                },
        //            },
        //        }
        //    };

        //return dispatches;
        //}
    }
}





