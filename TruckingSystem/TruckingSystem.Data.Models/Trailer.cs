using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static TruckingSystem.Common.ValidationConstants.TrailerConstants;

namespace TruckingSystem.Data.Models
{
    public class Trailer
    {
        public Trailer()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        [Comment("Unique Identifier")]
        public Guid Id { get; set; }

        [Comment("Trailer number")]
        public int TrailerNumber { get; set; }

        [Required]
        [MaxLength(TrailerMakeMaxLenght)]
        [Comment("Trailer make")]
        public string Make { get; set; } = null!;

        [Required]
        [MaxLength(TrailerTypeMaxLenght)]
        [Comment("Trailer type")]
        public string Type { get; set; } = null!;

        [Comment("Trailer production year")]
        public int ModelYear { get; set; }

        public Truck? Truck { get; set; }

        [Comment("Shows weather trailer is available or not")]
        public bool IsAvailable { get; set; } = true;

        [Comment("Shows weather trailer is deleted or not")]
        public bool IsDeleted { get; set; }
    }
}
