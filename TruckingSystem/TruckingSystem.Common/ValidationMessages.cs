namespace TruckingSystem.Common
{
    public static class ValidationMessages
    {
        public static class DriverValidationMessages
        {
            public const string DriverNameRequiredMessage = "The driver's name is required!";
            public const string DriverNameLengthErrorMessage = "The driver's name must be between 2 and 100 characters!";

            public const string DriverLicenseNumberRequiredMessage = "The driver's licence number is required!";
            public const string DriverLicenseNumberLenghtErrorMessage = "The driver's licence number must be between 5 and 20 characters!";
        }
    }
}
