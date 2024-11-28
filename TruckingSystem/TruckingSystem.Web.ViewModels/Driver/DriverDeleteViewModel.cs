namespace TruckingSystem.Web.ViewModels.Driver
{
	public class DriverDeleteViewModel
	{
        public Guid Id { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }
	}
}
