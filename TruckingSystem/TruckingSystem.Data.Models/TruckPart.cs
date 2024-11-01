using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TruckingSystem.Data.Models
{
    [PrimaryKey(nameof(TruckId), nameof(PartId))]
    public class TruckPart
    {
        [Comment("Game identifier")]
        public Guid TruckId { get; set; }

        [Required]
        [ForeignKey(nameof(TruckId))]
        public Truck Truck { get; set; } = null!;

        [Comment("Part identifier")]
        public Guid PartId { get; set; }

        [Required]
        [ForeignKey(nameof(PartId))]
        public Part Part { get; set; } = null!;
    }
}
