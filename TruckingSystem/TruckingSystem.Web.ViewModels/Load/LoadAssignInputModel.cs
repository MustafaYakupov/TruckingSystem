namespace TruckingSystem.Web.ViewModels.Load
{
    public class LoadAssignInputModel
    {
		public Guid LoadId { get; set; }

		public Guid DriverId { get; set; }

        public IEnumerable<Data.Models.Driver> Drivers { get; set; } = new List<Data.Models.Driver>();
    }
}
