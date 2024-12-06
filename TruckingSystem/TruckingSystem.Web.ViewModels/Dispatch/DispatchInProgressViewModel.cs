namespace TruckingSystem.Web.ViewModels.Dispatch
{
	public class DispatchInProgressViewModel
	{
		public Guid Id { get; set; }

		public required string Driver { get; set; }

		public required string Truck { get; set; }

		public required string Trailer { get; set; }

		public required string DriverManager { get; set; }

		public required string Load { get; set; }
	}
}
