using System.ComponentModel.DataAnnotations;
using static TruckingSystem.Common.ValidationConstants.TrailerConstants;
using static TruckingSystem.Common.ValidationMessages.TrailerValidationMessages;

namespace TruckingSystem.Web.ViewModels.Trailer
{
    public class TrailerAddInputModel
    {
        [Required(ErrorMessage = TrailerNumberRequiredMessage)]
        [StringLength(TrailerNumberMaxLenght, MinimumLength = TrailerNumberMinLenght, ErrorMessage = TrailerNumberLengthErrorMessage)]
        public string TrailerNumber { get; set; } = null!;

        [Required(ErrorMessage = TrailerMakeRequiredMessage)]
        [StringLength(TrailerMakeMaxLenght, MinimumLength = TrailerMakeMinLenght, ErrorMessage = TrailerMakeLengthErrorMessage)]
        public string Make { get; set; } = null!;

        [Required(ErrorMessage = TrailerTypeRequiredMessage)]
        [StringLength(TrailerTypeMaxLenght, MinimumLength = TrailerTypeMinLenght, ErrorMessage = TrailerTypeLengthErrorMessage)]
        public string Type { get; set; } = null!;

        [Required(ErrorMessage = TrailerModelYearRequiredMessage)]
        [StringLength(TrailerModelYearMaxLenght, MinimumLength = TrailerModelYearMinLenght, ErrorMessage = TrailerModelYearLengthErrorMessage)]
        public string ModelYear { get; set; } = null!;
    }
}
