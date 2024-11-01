using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static TruckingSystem.Common.ValidationConstants.DriverConstants;

namespace TruckingSystem.Data.Models
{
    public class Driver
    {
        public Driver()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        [Comment("Unique Identifier")]
        public Guid Id { get; set; }

        [Required]
        [Comment("Driver first name")]
        [MaxLength(DriverFirstAndLastNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [Comment("Driver last name")]
        [MaxLength(DriverFirstAndLastNameMaxLength)]
        public string LastName { get; set; } = null!;

        [Required]
        [Comment("Driver license number")]
        [MaxLength(DriverLicenseNumberMaxLength)]
        public string LicenseNumber { get; set; } = null!;

        [Comment("The Identifier of the driver's truck")]
        public Guid? TruckId { get; set; }

        [ForeignKey(nameof(TruckId))]
        public Truck? Truck { get; set; }

        [Comment("The Identifier of the driver's trailer")]
        public Guid? TrailerId { get; set; }

        [ForeignKey(nameof(TrailerId))]
        public Trailer? Trailer { get; set; }

        [Comment("The Identifier of the driver's Manager")]
        public Guid? DriverManagerId { get; set; }

        [ForeignKey(nameof(DriverManagerId))]   
        public DriverManager? DriverManager { get; set; }

        [Comment("Loads assigned on the driver")]
        public ICollection<Load> Loads { get; set; } = new HashSet<Load>();

        [Comment("Shows weather the driver is busy or available")]
        public bool IsAvailable { get; set; } = true;

        [Comment("Shows weather driver is deleted or not")]
        public bool IsDeleted { get; set; }
    }
}
