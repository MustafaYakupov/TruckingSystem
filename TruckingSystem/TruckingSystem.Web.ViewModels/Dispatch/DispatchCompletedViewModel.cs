namespace TruckingSystem.Web.ViewModels.Dispatch
{
    public class DispatchCompletedViewModel
    {
        public Guid Id { get; set; }

        public required string Driver { get; set; }

        public required string Truck { get; set; }

        public required string Trailer { get; set; }

        public required string DriverManager { get; set; }

        public required string PickupAddress { get; set; }

        public required string DeliveryAddress { get; set; }

        public required string PickupTime { get; set; }

        public required string DeliveryTime { get; set; }

        public int Distance { get; set; }

        public int Weight { get; set; }

        public required string Temperature { get; set; }

        public required string BrokerCompany { get; set; }
    }
}
