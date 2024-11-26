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

        [Required]
        [Comment("Trailer number")]
        [MaxLength(TrailerNumberMaxLenght)]
        public string TrailerNumber { get; set; } = null!;

        [Required]
        [MaxLength(TrailerMakeMaxLenght)]
        [Comment("Trailer make")]
        public string Make { get; set; } = null!;

        [Required]
        [MaxLength(TrailerTypeMaxLenght)]
        [Comment("Trailer type")]
        public string Type { get; set; } = null!;

        [Required]
        [Comment("Trailer production year")]
        [MaxLength(TrailerModelYearMaxLenght)]
        public string ModelYear { get; set; } = null!;

        public Truck? Truck { get; set; }

        [Comment("Shows weather trailer is available or not")]
        public bool IsAvailable { get; set; } = true;

        [Comment("Shows weather trailer is deleted or not")]
        public bool IsDeleted { get; set; }
    }
}
