namespace TruckingSystem.Web.ViewModels.Truck
{
    public class PartSelectionViewModel
    {
        public Guid PartId { get; set; }

        public required string PartType { get; set; }

        public required string PartMake { get; set; }

        public bool IsSelected { get; set; } = false;
    }
}
