using TruckingSystem.Data.Models;

namespace TruckingSystem.Web.ViewModels.Truck
{
    public class TruckAllViewModel
    {
        public Guid Id { get; set; }

        public required string TruckNumber { get; set; }

        public required string Make { get; set; }

        public required string Model { get; set; }

        public required string LicensePlate { get; set; }

        public required string ModelYear { get; set; }

        public required string Color { get; set; }

        public ICollection<TruckPart> TrucksParts { get; set; } = new List<TruckPart>();

    }
}
