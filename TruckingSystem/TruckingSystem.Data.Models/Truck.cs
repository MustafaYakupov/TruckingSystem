using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static TruckingSystem.Common.ValidationConstants.TruckConstants;

namespace TruckingSystem.Data.Models
{
    public class Truck
    {
        public Truck()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        [Comment("Unique Identifier")]
        public Guid Id { get; set; }

        [Comment("Truck number")]
        public int TruckNumber { get; set; }

        [Required]
        [Comment("Truck make")]
        [MaxLength(TruckMakeMaxLenght)]
        public string Make { get; set; } = null!;

        [Required]
        [Comment("Truck model")]
        [MaxLength(TruckModelMaxLenght)]
        public string Model { get; set; } = null!;

        [Required]
        [Comment("Truck license plate")]
        [MaxLength(TruckLicensePlateMaxLenght)]
        public string LicensePlate { get; set; } = null!;

        [Comment("Truck produciton year")]
        public int ModelYear { get; set; }

        [Required]
        [Comment("Truck color")]
        [MaxLength(TruckColorMaxLenght)]
        public string Color { get; set; } = null!;

        [Comment("Trailer identifier")]
        public Guid? TrailerId { get; set; }

        [ForeignKey(nameof(TrailerId))]
        public Trailer? Trailer { get; set; }

        public Driver? Driver { get; set; }

        public ICollection<TruckPart> TrucksParts { get; set; } = new HashSet<TruckPart>();

        [Comment("Shows weather truck is available or not")]
        public bool IsAvailable { get; set; } = true;

        [Comment("Shows weather truck is deleted or not")]
        public bool IsDeleted { get; set; } 
    }
}
