namespace TruckingSystem.Web.ViewModels.Driver
{
    public class DriverAllViewModel
    {
        public Guid Id { get; set; }

        public required string FirstName { get; set; } 

        public required string LastName { get; set; } 

        public required string LicenseNumber { get; set; }

        public string? TruckNumber { get; set; }

        public string? TrailerNumber { get; set; }

        public string? DriverManager { get; set; }
    }
}
