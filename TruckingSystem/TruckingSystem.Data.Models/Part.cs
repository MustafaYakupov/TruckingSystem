using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static TruckingSystem.Common.ValidationConstants.PartConstants;

namespace TruckingSystem.Data.Models
{
    public class Part
    {
        public Part()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        [Comment("Unique Identifier")]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(PartTypeMaxLength)]
        [Comment("Part type")]
        public string Type { get; set; } = null!;

        [Required]
        [MaxLength(PartMakeMaxLength)]
        [Comment("Part make")]
        public string Make { get; set; } = null!;

        public ICollection<TruckPart> TruckParts { get; set; } = new HashSet<TruckPart>();

        [Comment("Shows weather part is deleted or not")]
        public bool IsDeleted { get; set; }
    }
}
