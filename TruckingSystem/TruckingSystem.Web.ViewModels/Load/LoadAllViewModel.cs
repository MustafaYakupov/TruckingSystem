namespace TruckingSystem.Web.ViewModels.Load
{
    public class LoadAllViewModel
    {
        public Guid Id { get; set; }

        public required string PickupLocation { get; set; }
        public required string DeliveryLocation { get; set; }
        public  int Weight { get; set; }
        public  string? Temperature { get; set; }
        public required string PickupTime { get; set; }
        public required string DeliveryTime { get; set; }
        public int Distance { get; set; }
        public required string BrokerCompany { get; set; }
        public  string? Driver { get; set; }
    }
}
