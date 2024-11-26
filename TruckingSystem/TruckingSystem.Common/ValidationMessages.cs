namespace TruckingSystem.Common
{
    public static class ValidationMessages
    {
        public static class DriverValidationMessages
        {
            public const string DriverNameRequiredMessage = "Driver name is required.";
            public const string DriverNameTooShortMessage = "Driver name must be at least 2 characters.";
            public const string DriverNameTooLongMessage = "Driver name can't be longer than 100 characters.";

            public const string DriverLicenseNumberRequiredMessage = "Driver licence number is required.";
            public const string DriverLicenseNumberTooShortMessage = "Driver licence number must be at least 5 characters.";
            public const string DriverLicenseNumberTooLongMessage = "Driver licence number can't be longer than 20 characters.";
        }
    }
}
