using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TruckingSystem.Data.Models.Enums;

namespace TruckingSystem.Data.Models
{
    public class Dispatch
    {
        public Dispatch()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        [Comment("Unique Identifier")]
        public Guid Id { get; set; }

        [Comment("Unique Identifier")]
        public Guid DriverId { get; set; }

        [Required]
        public Driver Driver { get; set; } = null!;

        [Comment("Truck Unique Identifier")]
        public Guid TruckId { get; set; }

        [Required]
        public Truck Truck { get; set; } = null!;

        [Comment("Trailer Unique Identifier")]
        public Guid TrailerId { get; set; }

        [Required]
        public Trailer Trailer { get; set; } = null!;

        [Comment("DriverManager Unique Identifier")]
        public Guid DriverManagerId { get; set; }

        [Required]
        public DriverManager DriverManager { get; set; } = null!;

        [Comment("Load Unique Identifier")]
        public Guid LoadId { get; set; }

        [Required]
        public Load Load { get; set; } = null!;

        [Required]
        public DispatchStatus Status { get; set; } = 0;

        [Comment("Shows weather dispatch is deleted or not")]
        public bool IsDeleted { get; set; }
    }
}
