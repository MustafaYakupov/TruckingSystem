using System.ComponentModel.DataAnnotations;
using TruckingSystem.Data.Models;
using static TruckingSystem.Common.ValidationConstants.LoadConstants;
using static TruckingSystem.Common.ValidationMessages.LoadValidationMessages;

namespace TruckingSystem.Web.ViewModels.Load
{
    public class LoadAddInputModel
    {
        [Required(ErrorMessage = LoadPickupLocationRequiredMessage)]
        [StringLength(PickupLocationMaxLength, MinimumLength = PickupLocationMinLength, ErrorMessage = LoadPickupLocationLengthErrorMessage)]
        public string PickupLocation { get; set; } = null!;

        [Required(ErrorMessage = LoadDeliveryLocationRequiredMessage)]
        [StringLength(DeliveryLocationMaxLength, MinimumLength = DeliveryLocationMinLength, ErrorMessage = LoadDeliveryLocationRequiredMessage)]
        public string DeliveryLocation { get; set; } = null!;

        [Required(ErrorMessage = LoadWeightRequiredMessage)]
        public int Weight { get; set; } 

        public double? Temperature { get; set; }

        [Required(ErrorMessage = LoadPickupTimeRequiredMessage)]
        public string PickupTime { get; set; } = null!;

        [Required(ErrorMessage = LoadDeliveryTimeRequiredMessage)]
        public string DeliveryTime { get; set; } = null!;

        [Required(ErrorMessage = LoadDistanceRequiredMessage)]
        public int Distance { get; set; } 

        [Required(ErrorMessage = LoadBrokerCompanyRequiredMessage)]
        public Guid BrokerCompanyId { get; set; } 

        public IEnumerable<BrokerCompany> BrokerCompanies { get; set; } = new List<BrokerCompany>();
    }
}
