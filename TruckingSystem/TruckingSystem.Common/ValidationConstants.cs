namespace TruckingSystem.Common
{
    public static class ValidationConstants
    {
        public static class DriverConstants
        {
            public const byte DriverFirstAndLastNameMinLength = 2;
            public const byte DriverFirstAndLastNameMaxLength = 100;
            public const byte DriverLicenseNumberMinLength = 5;
            public const byte DriverLicenseNumberMaxLength = 20;
        }

        public static class BrokerCompanyConstants
        {
            public const byte CompanyNameMinLength = 2;
            public const byte CompanyNameMaxLength = 100;
        }

        public static class DriverManagerConstants
        {
            public const byte DriverManagerFirstAndLastNameMinLength = 2;
            public const byte DriverManagerFirstAndLastNameMaxLength = 100;
        }

        public static class LoadConstants
        {
            public const byte PickupLocationMinLength = 5;
            public const byte PickupLocationMaxLength = 255;
            public const byte DeliveryLocationMinLength = 5;
            public const byte DeliveryLocationMaxLength = 255;
            public const string DateTimeFormat = "MMMM/dd/yyyy";
        }

        public static class PartConstants
        {
            public const byte PartTypeMinLength = 2;
            public const byte PartTypeMaxLength = 50;
            public const byte PartMakeMinLength = 2;
            public const byte PartMakeMaxLength = 100;
        }

        public static class TruckConstants
        {
            public const byte TruckMakeMinLenght = 3;
            public const byte TruckMakeMaxLenght = 50;
            public const byte TruckModelMinLenght = 3;
            public const byte TruckModelMaxLenght = 50;
            public const byte TruckLicensePlateMinLenght = 3;
            public const byte TruckLicensePlateMaxLenght = 20;
            public const byte TruckColorMinLenght = 3;
            public const byte TruckColorMaxLenght = 20;
        }

        public static class TrailerConstants
        {
            public const byte TrailerMakeMinLenght = 3;
            public const byte TrailerMakeMaxLenght = 50;
            public const byte TrailerTypeMinLenght = 3;
            public const byte TrailerTypeMaxLenght = 10;
        }
    }
}
