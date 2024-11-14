using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TruckingSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BrokerCompanies",
                columns: new[] { "Id", "CompanyName", "IsDeleted" },
                values: new object[,]
                {
                    { new Guid("18f9e2c7-6e29-4bb7-b7d4-5383d8c9f1a7"), "Global Trucking Co.", false },
                    { new Guid("59c6e7d3-8aef-40bc-94c4-0f2f7a8f4d4f"), "Allied Freight Solutions", false },
                    { new Guid("91a8b3d5-6319-4e09-9a5b-d4bb01c47bcf"), "Freight Partners LLC", false },
                    { new Guid("a3f06f5a-99f8-4c5b-a4e4-323e543245b3"), "Swift Transports", false },
                    { new Guid("e01235bc-6de9-4af2-a6f8-d431a40bc9a3"), "Road King Logistics", false }
                });

            migrationBuilder.InsertData(
                table: "DriverManagers",
                columns: new[] { "Id", "FirstName", "IsDeleted", "LastName" },
                values: new object[,]
                {
                    { new Guid("b29f8c7f-5fa1-42e3-a0d7-4e6f9c9b8b23"), "Michael", false, "Johnson" },
                    { new Guid("c2af7c54-9e28-48b9-a7f6-3b47a4a8e1d0"), "Chris", false, "Taylor" },
                    { new Guid("d237e2c6-77fc-4f38-823d-9a4a3d5f230c"), "John", false, "Doe" },
                    { new Guid("e17b45d5-99c3-4d2b-b894-8b4a543d8d27"), "Alice", false, "Smith" },
                    { new Guid("f12d25ef-3b21-4f4b-baf2-6d3e7a9c1a99"), "Emma", false, "Brown" }
                });

            migrationBuilder.InsertData(
                table: "Drivers",
                columns: new[] { "Id", "DriverManagerId", "FirstName", "IsAvailable", "IsDeleted", "LastName", "LicenseNumber", "TrailerId", "TruckId" },
                values: new object[,]
                {
                    { new Guid("3b9a8f4b-812e-40c2-b7e3-f8a5c6f2b901"), null, "James", true, false, "Brown", "JMB123456", null, null },
                    { new Guid("b1d4c7f4-8a2b-4c3e-b91d-1c3f7a5b2e90"), null, "Mia", true, false, "Hernandez", "MIH890123", null, null },
                    { new Guid("b2912d4e-8b21-4ef3-b02d-2b6f0a1c8d32"), null, "Oliver", true, false, "Johnson", "OLJ456789", null, null },
                    { new Guid("b2f7a4e5-8c2d-41e3-b02f-7a6e3d1b5c31"), null, "Henry", true, false, "Thomas", "HET345678", null, null },
                    { new Guid("b5a7f4e1-9c2a-40e2-b71a-2a6c8d3f1b81"), null, "Noah", true, false, "Martinez", "NOM901234", null, null },
                    { new Guid("c5f3b4e1-8d2f-41a3-b91d-7a6c8e1d3b41"), null, "Benjamin", true, false, "White", "BEW456901", null, null },
                    { new Guid("c6b4a2d3-8d2f-41f3-a03c-3a5d8b7e2c61"), null, "William", true, false, "Rodriguez", "WIR567890", null, null },
                    { new Guid("c7a8b34f-9f4a-43d2-a73f-7b4a5b2e3b20"), null, "Liam", true, false, "Jones", "LIJ345678", null, null },
                    { new Guid("c9b1f5e3-8f2c-41d2-a92d-6b7a5e2d3c12"), null, "Amelia", true, false, "Gonzalez", "AMG345012", null, null },
                    { new Guid("d2a4e7b5-8b3f-4c2d-b51c-4a7f8e1b0c31"), null, "Alexander", true, false, "Wilson", "ALW678134", null, null },
                    { new Guid("d7c5b4e2-9a2f-41c3-b03d-6e7b3f4d1a61"), null, "Michael", true, false, "Moore", "MIC890456", null, null },
                    { new Guid("d8a3c7b5-9b3a-4f2d-b02e-2f5e7b4c6d12"), null, "Sofia", true, false, "Harris", "SOH789345", null, null },
                    { new Guid("e1928b4e-8f31-41e3-b03d-4f7a3a6c9b21"), null, "Sophia", true, false, "Williams", "SOP012345", null, null },
                    { new Guid("e2b4c3f1-9d2e-4f3f-a01c-5d3a4b7e2f01"), null, "Evelyn", true, false, "Jackson", "EVJ012389", null, null },
                    { new Guid("e3b7d5f2-8b1c-42e3-a92d-4c7f3b5e0a41"), null, "Isabella", true, false, "Davis", "ISD234567", null, null },
                    { new Guid("e3c6b7f1-9d3a-4f2e-a82d-2b7a4d6e5b21"), null, "Charlotte", true, false, "Anderson", "CHA901456", null, null },
                    { new Guid("f18b8f4d-9b1a-4bc2-a729-9f12a8c2b721"), null, "Lily", true, false, "Smith", "LYS789012", null, null },
                    { new Guid("f3b2c6a1-8f3d-4c2f-b21d-3a7f6b4e1c72"), null, "Ella", true, false, "Taylor", "ELT678134", null, null },
                    { new Guid("f4b3c6d5-9a1f-4d3f-b12d-3b5e6f2a7c01"), null, "Lucas", true, false, "Lopez", "LUL123890", null, null },
                    { new Guid("f9b3a2f0-9d2b-4e3f-b02c-8a5b6b3e1a71"), null, "Emma", true, false, "Garcia", "EMG678901", null, null }
                });

            migrationBuilder.InsertData(
                table: "Parts",
                columns: new[] { "Id", "IsDeleted", "Make", "Type" },
                values: new object[,]
                {
                    { new Guid("4ed2e3d8-958d-4629-89f4-7e2b8d8d86b7"), false, "Allison", "Transmission" },
                    { new Guid("5a6ed2b7-b7a4-4fd9-8c49-b2e1d9f7c7b2"), false, "Hendrickson", "Suspension" },
                    { new Guid("8c49d3b5-3287-43f6-a6f1-2d8c9b1a7b8e"), false, "Bendix", "Brake System" },
                    { new Guid("9d6ba2c5-d2e9-4c9b-8d8b-a2f9c6e8d7b3"), false, "Exide", "Battery" },
                    { new Guid("f24baf3d-1f3c-4329-aafd-ea5cb99e5b6a"), false, "Cummins", "Engine" }
                });

            migrationBuilder.InsertData(
                table: "Trailers",
                columns: new[] { "Id", "IsAvailable", "IsDeleted", "Make", "ModelYear", "TrailerNumber", "Type" },
                values: new object[,]
                {
                    { new Guid("263fe327-e883-468e-83b3-ff072b38944a"), true, false, "Thermo King", 2020, 504, "Reefer" },
                    { new Guid("47c3d1b7-9fa9-4ca6-9549-b9742fcf85cb"), true, false, "Great Dane", 2021, 506, "Reefer" },
                    { new Guid("54830d70-bc69-4539-bd97-36d2093dfd0e"), true, false, "Carrier", 2018, 505, "Reefer" },
                    { new Guid("83274bc8-bef1-47ab-bbb1-b4422eae8d44"), true, false, "Utility", 2022, 503, "Reefer" },
                    { new Guid("a189e23d-8eb9-4b83-b6ed-61ec5d8b04bc"), true, false, "Utility", 2019, 507, "Reefer" },
                    { new Guid("ae48c5b1-2c1f-4a7b-8cc1-4b89d7cc9f42"), true, false, "Utility", 2021, 500, "Reefer" },
                    { new Guid("bfaad345-3a44-4d9a-b015-7f7b4f85d3a1"), true, false, "Carrier", 2019, 502, "Reefer" },
                    { new Guid("c3e8b097-6340-45b0-8eb0-9578f8409f52"), true, false, "Great Dane", 2020, 501, "Reefer" },
                    { new Guid("cb49e342-d2b3-4b2b-9bb9-c2346d823dc0"), true, false, "Carrier", 2022, 508, "Reefer" },
                    { new Guid("d1b99cfa-cf73-4786-ae4a-4a49d1b179b5"), true, false, "Great Dane", 2020, 509, "Reefer" }
                });

            migrationBuilder.InsertData(
                table: "Trucks",
                columns: new[] { "Id", "Color", "IsAvailable", "IsDeleted", "LicensePlate", "Make", "Model", "ModelYear", "TrailerId", "TruckNumber" },
                values: new object[,]
                {
                    { new Guid("1c5a5bcf-0d38-4b2b-a8d2-b306a6d8b7da"), "White", true, false, "LT101FL", "Freightliner", "Cascadia", 2021, null, 101 },
                    { new Guid("5c1f054b-efef-4ee6-81cc-9f41e7c02ea2"), "Silver", true, false, "LT105MK", "Mack", "Anthem", 2021, null, 105 },
                    { new Guid("bbbe9c6e-cda4-4b80-8988-e7456020dfe7"), "Blue", true, false, "LT102VL", "Volvo", "VNL 760", 2020, null, 102 },
                    { new Guid("be8b8f36-0cfe-4509-98b5-2e779a9f8b07"), "Yellow", true, false, "LT106IN", "International", "LT", 2020, null, 106 },
                    { new Guid("bfa9f0a5-c189-4cc9-8e65-dfa218ec4f60"), "Red", true, false, "LT110PB", "Peterbilt", "389", 2020, null, 110 },
                    { new Guid("c303b365-bb6e-4a43-b473-61d25a9e39d3"), "Blue", true, false, "LT109VL", "Volvo", "VNL 860", 2022, null, 109 },
                    { new Guid("cd0f5805-9409-420b-b6a3-740dfdba84a0"), "White", true, false, "LT108KW", "Kenworth", "W990", 2021, null, 108 },
                    { new Guid("d1fda9c2-5a0a-4ca0-8a3d-b417c40d68c9"), "Red", true, false, "LT103KW", "Kenworth", "T680", 2019, null, 103 },
                    { new Guid("d833d70d-e77e-4e39-b700-0a743b2f1ed6"), "Black", true, false, "LT104PB", "Peterbilt", "579", 2022, null, 104 },
                    { new Guid("f4b963fd-1d56-4296-92a6-c92366a67bfc"), "Gray", true, false, "LT107FL", "Freightliner", "Coronado", 2018, null, 107 }
                });

            migrationBuilder.InsertData(
                table: "Loads",
                columns: new[] { "Id", "BrokerCompanyId", "DeliveryLocation", "DeliveryTime", "Distance", "DriverId", "IsAvailable", "IsDeleted", "PickupLocation", "PickupTime", "Temperature", "Weight" },
                values: new object[,]
                {
                    { new Guid("0f3a4d6f-8b2d-47a5-b8c7-f2d4e8f5b8a7"), new Guid("18f9e2c7-6e29-4bb7-b7d4-5383d8c9f1a7"), "Indianapolis, IN", new DateTime(2024, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 290, null, true, false, "Nashville, TN", new DateTime(2024, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), -10.0, 13000 },
                    { new Guid("3a9e2b56-957a-41a6-9bbd-b45d3f0d5e6f"), new Guid("91a8b3d5-6319-4e09-9a5b-d4bb01c47bcf"), "Phoenix, AZ", new DateTime(2024, 12, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 600, null, true, false, "Denver, CO", new DateTime(2024, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 10.0, 16000 },
                    { new Guid("4b8c2d65-a3c5-4f7e-9943-ec2d3d48f88e"), new Guid("91a8b3d5-6319-4e09-9a5b-d4bb01c47bcf"), "San Francisco, CA", new DateTime(2024, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 800, null, true, false, "Seattle, WA", new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 8.0, 21000 },
                    { new Guid("5c3f2b57-9f4d-4d85-b6f2-e73e5d88c77d"), new Guid("e01235bc-6de9-4af2-a6f8-d431a40bc9a3"), "Orlando, FL", new DateTime(2024, 12, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 440, null, true, false, "Atlanta, GA", new DateTime(2024, 12, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 4.0, 12000 },
                    { new Guid("6a7d5c4f-b4a6-44c6-af7d-9f2c3f88d4a3"), new Guid("e01235bc-6de9-4af2-a6f8-d431a40bc9a3"), "Dallas, TX", new DateTime(2025, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 1330, null, true, false, "Miami, FL", new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 12.0, 19000 },
                    { new Guid("7b8f2e6c-4c5f-4d66-9bbd-b47e3e88f22d"), new Guid("59c6e7d3-8aef-40bc-94c4-0f2f7a8f4d4f"), "Philadelphia, PA", new DateTime(2025, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 300, null, true, false, "Boston, MA", new DateTime(2025, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), -2.0, 14000 },
                    { new Guid("8c2f3a4e-5f3a-4f98-a3b6-b38e7e4d5e6f"), new Guid("59c6e7d3-8aef-40bc-94c4-0f2f7a8f4d4f"), "Salt Lake City, UT", new DateTime(2024, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 420, null, true, false, "Las Vegas, NV", new DateTime(2024, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 20.0, 22000 },
                    { new Guid("9d6f5e3a-7d3b-45c7-b8e2-5f4a2f88e4f5"), new Guid("18f9e2c7-6e29-4bb7-b7d4-5383d8c9f1a7"), "Minneapolis, MN", new DateTime(2024, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 440, null, true, false, "Kansas City, MO", new DateTime(2024, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 5.0, 16000 },
                    { new Guid("e2e8fc12-5a4e-4d41-b8f2-c6ae5ea84600"), new Guid("a3f06f5a-99f8-4c5b-a4e4-323e543245b3"), "Los Angeles, CA", new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 2800, null, true, false, "New York, NY", new DateTime(2025, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 15.0, 20000 },
                    { new Guid("f1c9d2e4-8b8d-43a6-baf3-d48e51d4920a"), new Guid("a3f06f5a-99f8-4c5b-a4e4-323e543245b3"), "Houston, TX", new DateTime(2024, 12, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 1080, null, true, false, "Chicago, IL", new DateTime(2024, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), -5.0, 18000 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DriverManagers",
                keyColumn: "Id",
                keyValue: new Guid("b29f8c7f-5fa1-42e3-a0d7-4e6f9c9b8b23"));

            migrationBuilder.DeleteData(
                table: "DriverManagers",
                keyColumn: "Id",
                keyValue: new Guid("c2af7c54-9e28-48b9-a7f6-3b47a4a8e1d0"));

            migrationBuilder.DeleteData(
                table: "DriverManagers",
                keyColumn: "Id",
                keyValue: new Guid("d237e2c6-77fc-4f38-823d-9a4a3d5f230c"));

            migrationBuilder.DeleteData(
                table: "DriverManagers",
                keyColumn: "Id",
                keyValue: new Guid("e17b45d5-99c3-4d2b-b894-8b4a543d8d27"));

            migrationBuilder.DeleteData(
                table: "DriverManagers",
                keyColumn: "Id",
                keyValue: new Guid("f12d25ef-3b21-4f4b-baf2-6d3e7a9c1a99"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("3b9a8f4b-812e-40c2-b7e3-f8a5c6f2b901"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("b1d4c7f4-8a2b-4c3e-b91d-1c3f7a5b2e90"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("b2912d4e-8b21-4ef3-b02d-2b6f0a1c8d32"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("b2f7a4e5-8c2d-41e3-b02f-7a6e3d1b5c31"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("b5a7f4e1-9c2a-40e2-b71a-2a6c8d3f1b81"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("c5f3b4e1-8d2f-41a3-b91d-7a6c8e1d3b41"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("c6b4a2d3-8d2f-41f3-a03c-3a5d8b7e2c61"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("c7a8b34f-9f4a-43d2-a73f-7b4a5b2e3b20"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("c9b1f5e3-8f2c-41d2-a92d-6b7a5e2d3c12"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("d2a4e7b5-8b3f-4c2d-b51c-4a7f8e1b0c31"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("d7c5b4e2-9a2f-41c3-b03d-6e7b3f4d1a61"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("d8a3c7b5-9b3a-4f2d-b02e-2f5e7b4c6d12"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("e1928b4e-8f31-41e3-b03d-4f7a3a6c9b21"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("e2b4c3f1-9d2e-4f3f-a01c-5d3a4b7e2f01"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("e3b7d5f2-8b1c-42e3-a92d-4c7f3b5e0a41"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("e3c6b7f1-9d3a-4f2e-a82d-2b7a4d6e5b21"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("f18b8f4d-9b1a-4bc2-a729-9f12a8c2b721"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("f3b2c6a1-8f3d-4c2f-b21d-3a7f6b4e1c72"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("f4b3c6d5-9a1f-4d3f-b12d-3b5e6f2a7c01"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("f9b3a2f0-9d2b-4e3f-b02c-8a5b6b3e1a71"));

            migrationBuilder.DeleteData(
                table: "Loads",
                keyColumn: "Id",
                keyValue: new Guid("0f3a4d6f-8b2d-47a5-b8c7-f2d4e8f5b8a7"));

            migrationBuilder.DeleteData(
                table: "Loads",
                keyColumn: "Id",
                keyValue: new Guid("3a9e2b56-957a-41a6-9bbd-b45d3f0d5e6f"));

            migrationBuilder.DeleteData(
                table: "Loads",
                keyColumn: "Id",
                keyValue: new Guid("4b8c2d65-a3c5-4f7e-9943-ec2d3d48f88e"));

            migrationBuilder.DeleteData(
                table: "Loads",
                keyColumn: "Id",
                keyValue: new Guid("5c3f2b57-9f4d-4d85-b6f2-e73e5d88c77d"));

            migrationBuilder.DeleteData(
                table: "Loads",
                keyColumn: "Id",
                keyValue: new Guid("6a7d5c4f-b4a6-44c6-af7d-9f2c3f88d4a3"));

            migrationBuilder.DeleteData(
                table: "Loads",
                keyColumn: "Id",
                keyValue: new Guid("7b8f2e6c-4c5f-4d66-9bbd-b47e3e88f22d"));

            migrationBuilder.DeleteData(
                table: "Loads",
                keyColumn: "Id",
                keyValue: new Guid("8c2f3a4e-5f3a-4f98-a3b6-b38e7e4d5e6f"));

            migrationBuilder.DeleteData(
                table: "Loads",
                keyColumn: "Id",
                keyValue: new Guid("9d6f5e3a-7d3b-45c7-b8e2-5f4a2f88e4f5"));

            migrationBuilder.DeleteData(
                table: "Loads",
                keyColumn: "Id",
                keyValue: new Guid("e2e8fc12-5a4e-4d41-b8f2-c6ae5ea84600"));

            migrationBuilder.DeleteData(
                table: "Loads",
                keyColumn: "Id",
                keyValue: new Guid("f1c9d2e4-8b8d-43a6-baf3-d48e51d4920a"));

            migrationBuilder.DeleteData(
                table: "Parts",
                keyColumn: "Id",
                keyValue: new Guid("4ed2e3d8-958d-4629-89f4-7e2b8d8d86b7"));

            migrationBuilder.DeleteData(
                table: "Parts",
                keyColumn: "Id",
                keyValue: new Guid("5a6ed2b7-b7a4-4fd9-8c49-b2e1d9f7c7b2"));

            migrationBuilder.DeleteData(
                table: "Parts",
                keyColumn: "Id",
                keyValue: new Guid("8c49d3b5-3287-43f6-a6f1-2d8c9b1a7b8e"));

            migrationBuilder.DeleteData(
                table: "Parts",
                keyColumn: "Id",
                keyValue: new Guid("9d6ba2c5-d2e9-4c9b-8d8b-a2f9c6e8d7b3"));

            migrationBuilder.DeleteData(
                table: "Parts",
                keyColumn: "Id",
                keyValue: new Guid("f24baf3d-1f3c-4329-aafd-ea5cb99e5b6a"));

            migrationBuilder.DeleteData(
                table: "Trailers",
                keyColumn: "Id",
                keyValue: new Guid("263fe327-e883-468e-83b3-ff072b38944a"));

            migrationBuilder.DeleteData(
                table: "Trailers",
                keyColumn: "Id",
                keyValue: new Guid("47c3d1b7-9fa9-4ca6-9549-b9742fcf85cb"));

            migrationBuilder.DeleteData(
                table: "Trailers",
                keyColumn: "Id",
                keyValue: new Guid("54830d70-bc69-4539-bd97-36d2093dfd0e"));

            migrationBuilder.DeleteData(
                table: "Trailers",
                keyColumn: "Id",
                keyValue: new Guid("83274bc8-bef1-47ab-bbb1-b4422eae8d44"));

            migrationBuilder.DeleteData(
                table: "Trailers",
                keyColumn: "Id",
                keyValue: new Guid("a189e23d-8eb9-4b83-b6ed-61ec5d8b04bc"));

            migrationBuilder.DeleteData(
                table: "Trailers",
                keyColumn: "Id",
                keyValue: new Guid("ae48c5b1-2c1f-4a7b-8cc1-4b89d7cc9f42"));

            migrationBuilder.DeleteData(
                table: "Trailers",
                keyColumn: "Id",
                keyValue: new Guid("bfaad345-3a44-4d9a-b015-7f7b4f85d3a1"));

            migrationBuilder.DeleteData(
                table: "Trailers",
                keyColumn: "Id",
                keyValue: new Guid("c3e8b097-6340-45b0-8eb0-9578f8409f52"));

            migrationBuilder.DeleteData(
                table: "Trailers",
                keyColumn: "Id",
                keyValue: new Guid("cb49e342-d2b3-4b2b-9bb9-c2346d823dc0"));

            migrationBuilder.DeleteData(
                table: "Trailers",
                keyColumn: "Id",
                keyValue: new Guid("d1b99cfa-cf73-4786-ae4a-4a49d1b179b5"));

            migrationBuilder.DeleteData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("1c5a5bcf-0d38-4b2b-a8d2-b306a6d8b7da"));

            migrationBuilder.DeleteData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("5c1f054b-efef-4ee6-81cc-9f41e7c02ea2"));

            migrationBuilder.DeleteData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("bbbe9c6e-cda4-4b80-8988-e7456020dfe7"));

            migrationBuilder.DeleteData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("be8b8f36-0cfe-4509-98b5-2e779a9f8b07"));

            migrationBuilder.DeleteData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("bfa9f0a5-c189-4cc9-8e65-dfa218ec4f60"));

            migrationBuilder.DeleteData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("c303b365-bb6e-4a43-b473-61d25a9e39d3"));

            migrationBuilder.DeleteData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("cd0f5805-9409-420b-b6a3-740dfdba84a0"));

            migrationBuilder.DeleteData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("d1fda9c2-5a0a-4ca0-8a3d-b417c40d68c9"));

            migrationBuilder.DeleteData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("d833d70d-e77e-4e39-b700-0a743b2f1ed6"));

            migrationBuilder.DeleteData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("f4b963fd-1d56-4296-92a6-c92366a67bfc"));

            migrationBuilder.DeleteData(
                table: "BrokerCompanies",
                keyColumn: "Id",
                keyValue: new Guid("18f9e2c7-6e29-4bb7-b7d4-5383d8c9f1a7"));

            migrationBuilder.DeleteData(
                table: "BrokerCompanies",
                keyColumn: "Id",
                keyValue: new Guid("59c6e7d3-8aef-40bc-94c4-0f2f7a8f4d4f"));

            migrationBuilder.DeleteData(
                table: "BrokerCompanies",
                keyColumn: "Id",
                keyValue: new Guid("91a8b3d5-6319-4e09-9a5b-d4bb01c47bcf"));

            migrationBuilder.DeleteData(
                table: "BrokerCompanies",
                keyColumn: "Id",
                keyValue: new Guid("a3f06f5a-99f8-4c5b-a4e4-323e543245b3"));

            migrationBuilder.DeleteData(
                table: "BrokerCompanies",
                keyColumn: "Id",
                keyValue: new Guid("e01235bc-6de9-4af2-a6f8-d431a40bc9a3"));
        }
    }
}
