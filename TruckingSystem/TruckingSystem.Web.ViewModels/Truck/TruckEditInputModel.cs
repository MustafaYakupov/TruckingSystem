using System.ComponentModel.DataAnnotations;
using TruckingSystem.Data.Models;
using static TruckingSystem.Common.ValidationConstants.TruckConstants;
using static TruckingSystem.Common.ValidationMessages.TruckValidationMessages;

namespace TruckingSystem.Web.ViewModels.Truck
{
    public class TruckEditInputModel
    {
        [Required(ErrorMessage = TruckNumberRequiredMessage)]
        [StringLength(TruckNumberMaxLenght, MinimumLength = TruckNumberMinLenght, ErrorMessage = TruckNumberLengthErrorMessage)]
        public string TruckNumber { get; set; } = null!;

        [Required(ErrorMessage = TruckMakeRequiredMessage)]
        [StringLength(TruckMakeMaxLenght, MinimumLength = TruckMakeMinLenght, ErrorMessage = TruckMakeLengthErrorMessage)]
        public string Make { get; set; } = null!;

        [Required(ErrorMessage = TruckModelRequiredMessage)]
        [StringLength(TruckModelMaxLenght, MinimumLength = TruckModelMinLenght, ErrorMessage = TruckModelLengthErrorMessage)]
        public string Model { get; set; } = null!;

        [Required(ErrorMessage = TruckLicensePlateRequiredMessage)]
        [StringLength(TruckLicensePlateMaxLenght, MinimumLength = TruckLicensePlateMinLenght, ErrorMessage = TruckLicensePlateLengthErrorMessage)]
        public string LicensePlate { get; set; } = null!;

        [Required(ErrorMessage = TruckModelYearRequiredMessage)]
        [StringLength(TruckModelYearMaxLenght, MinimumLength = TruckModelYearMinLenght, ErrorMessage = TruckModelYearErrorMessage)]
        public string ModelYear { get; set; } = null!;

        [Required(ErrorMessage = TruckColorRequiredMessage)]
        [StringLength(TruckNumberMaxLenght, MinimumLength = TruckNumberMinLenght, ErrorMessage = TruckColorLengthErrorMessage)]
        public string Color { get; set; } = null!;

        public Guid? PartId { get; set; }

        public IList<PartSelectionViewModel> Parts { get; set; } = new List<PartSelectionViewModel>();

	}
}
