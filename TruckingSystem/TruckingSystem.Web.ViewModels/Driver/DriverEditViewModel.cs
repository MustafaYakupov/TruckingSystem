using TruckingSystem.Data.Models;
using System.ComponentModel.DataAnnotations;
using static TruckingSystem.Common.ValidationConstants.DriverConstants;
using static TruckingSystem.Common.ValidationMessages.DriverValidationMessages;

namespace TruckingSystem.Web.ViewModels.Driver
{
    public class DriverEditViewModel
    {
        [Required(ErrorMessage = DriverNameRequiredMessage)]
        [StringLength(DriverFirstAndLastNameMaxLength, MinimumLength = DriverFirstAndLastNameMinLength, ErrorMessage = DriverNameLengthErrorMessage)]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = DriverNameRequiredMessage)]
        [StringLength(DriverFirstAndLastNameMaxLength, MinimumLength = DriverFirstAndLastNameMinLength, ErrorMessage = DriverNameLengthErrorMessage)]
        public required string LastName { get; set; }

        [Required(ErrorMessage = DriverLicenseNumberRequiredMessage)]
        [StringLength(DriverLicenseNumberMaxLength, MinimumLength = DriverLicenseNumberMinLength, ErrorMessage = DriverLicenseNumberLenghtErrorMessage)]
        public required string LicenseNumber { get; set; }

        public string? TruckNumber { get; set; }

        public string? TrailerNumber { get; set; }

		public Guid? TruckId { get; set; }

        public Guid? TrailerId { get; set; }

        public Guid? DriverManagerId { get; set; }

        public IEnumerable<Truck> AvailableTrucks { get; set; } = new List<Truck>();  

        public IEnumerable<Trailer> AvailableTrailers { get; set; } = new List<Trailer>();  

        public IEnumerable<DriverManager> DriverManagers { get; set; } = new List<DriverManager>();
    }
}
