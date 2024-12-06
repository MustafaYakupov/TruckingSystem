namespace TruckingSystem.Web.ViewModels.Load
{
	public class LoadDeleteViewModel
	{
		public Guid Id { get; set; }

		public required string PickupLocation { get; set; }

		public required string DeliveryLocation { get; set; }
	}
}
