namespace TruckingSystem.Web.ViewModels.Truck
{
    public class TruckDeleteViewModel
    {
        public Guid Id { get; set; }

        public required string TruckNumber { get; set; }
    }
}
