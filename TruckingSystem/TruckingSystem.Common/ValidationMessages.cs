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

        public static class TrailerValidationMessages
        {
            public const string TrailerNumberRequiredMessage = "The trailer number is required!";
            public const string TrailerNumberLengthErrorMessage = "The trailer number must be between 1 and 10 characters!";

            public const string TrailerMakeRequiredMessage = "The trailer make is required!";
            public const string TrailerMakeLengthErrorMessage = "The trailer make must be between 3 and 50 characters!";


            public const string TrailerTypeRequiredMessage = "The trailer type is required!";
            public const string TrailerTypeLengthErrorMessage = "The trailer type must be between 3 and 50 characters!";

            public const string TrailerModelYearRequiredMessage = "The trailer model year is required!";
            public const string TrailerModelYearLengthErrorMessage = "The trailer model year must be 4 characters!";
        }

        public static class TruckValidationMessages
        {
            public const string TruckNumberRequiredMessage = "The truck number is required!";
            public const string TruckNumberLengthErrorMessage = "The truck number must be between 1 and 10 characters!";

            public const string TruckMakeRequiredMessage = "The truck make is required!";
            public const string TruckMakeLengthErrorMessage = "The truck make must be between 3 and 50 characters!";


            public const string TruckModelRequiredMessage = "The truck model is required!";
            public const string TruckModelLengthErrorMessage = "The truck model must be between 3 and 50 characters!";


            public const string TruckLicensePlateRequiredMessage = "The license plate number is required!";
            public const string TruckLicensePlateLengthErrorMessage = "The truck License Plate must be between 3 and 20 characters!";


            public const string TruckModelYearRequiredMessage = "The truck model year is required!";
            public const string TruckModelYearErrorMessage = "The truck model year must 4 characters!";


            public const string TruckColorRequiredMessage = "The truck color is required!";
            public const string TruckColorLengthErrorMessage = "The truck color must be between 3 and 20 characters!";

        }
    }
}
