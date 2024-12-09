using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TruckingSystem.Data.Models.Enums;
using static TruckingSystem.Common.ValidationConstants.LoadConstants;

namespace TruckingSystem.Data.Models
{
    public class Load
    {
        public Load()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        [Comment("Unique Identifier")]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(PickupLocationMaxLength)]
        [Comment("The address where the load is shipping from")]
        public string PickupLocation { get; set; } = null!;

        [Required]
        [MaxLength(DeliveryLocationMaxLength)]
        [Comment("The address where the load is going to")]
        public string DeliveryLocation { get; set; }

        [Comment("Weight of the product")]
        public int Weight { get; set; }

        [Comment("Temperature the product must be kept at")]
        public double? Temperature { get; set; }

        [Comment("Pick up appointment time")]
        public DateTime PickupTime { get; set; }

        [Comment("Delivery appointment time")]
        public DateTime DeliveryTime { get; set; }

        [Comment("Load distance")]
        public int Distance { get; set; }

        [Comment("BrokerCompany identifier")]
        public Guid BrokerCompanyId { get; set; }

        [Required]
        [ForeignKey(nameof(BrokerCompanyId))]
        public BrokerCompany BrokerCompany { get; set; } = null!;

		[Comment("Driver identifier")]
		public Guid? DriverId { get; set; }

		[ForeignKey(nameof(DriverId))]
		public Driver? Driver { get; set; }

		[Comment("Shows the status of the load")]
        public DispatchStatus Status { get; set; } = DispatchStatus.Available;

        [Comment("Shows weather load is deleted or not")]
        public bool IsDeleted { get; set; }
    }
}
