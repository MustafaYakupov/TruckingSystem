namespace TruckingSystem.Web.ViewModels.Trailer
{
    public class TrailerAllViewModel
    {
        public Guid Id { get; set; }

        public required string TrailerNumber { get; set; }

        public required string Make { get; set; }

        public required string Type { get; set; }

        public required string ModelYear { get; set; }
    }
}
