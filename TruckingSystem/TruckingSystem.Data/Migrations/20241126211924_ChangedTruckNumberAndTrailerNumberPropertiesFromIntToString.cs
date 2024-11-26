using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckingSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedTruckNumberAndTrailerNumberPropertiesFromIntToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TruckNumber",
                table: "Trucks",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                comment: "Truck number",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Truck number");

            migrationBuilder.AlterColumn<string>(
                name: "ModelYear",
                table: "Trucks",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: false,
                comment: "Truck produciton year",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Truck produciton year");

            migrationBuilder.AlterColumn<string>(
                name: "TrailerNumber",
                table: "Trailers",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                comment: "Trailer number",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Trailer number");

            migrationBuilder.AlterColumn<string>(
                name: "ModelYear",
                table: "Trailers",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: false,
                comment: "Trailer production year",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Trailer production year");

            migrationBuilder.UpdateData(
                table: "Trailers",
                keyColumn: "Id",
                keyValue: new Guid("263fe327-e883-468e-83b3-ff072b38944a"),
                columns: new[] { "ModelYear", "TrailerNumber" },
                values: new object[] { "2020", "504" });

            migrationBuilder.UpdateData(
                table: "Trailers",
                keyColumn: "Id",
                keyValue: new Guid("47c3d1b7-9fa9-4ca6-9549-b9742fcf85cb"),
                columns: new[] { "ModelYear", "TrailerNumber" },
                values: new object[] { "2021", "506" });

            migrationBuilder.UpdateData(
                table: "Trailers",
                keyColumn: "Id",
                keyValue: new Guid("54830d70-bc69-4539-bd97-36d2093dfd0e"),
                columns: new[] { "ModelYear", "TrailerNumber" },
                values: new object[] { "2018", "505" });

            migrationBuilder.UpdateData(
                table: "Trailers",
                keyColumn: "Id",
                keyValue: new Guid("83274bc8-bef1-47ab-bbb1-b4422eae8d44"),
                columns: new[] { "ModelYear", "TrailerNumber" },
                values: new object[] { "2022", "503" });

            migrationBuilder.UpdateData(
                table: "Trailers",
                keyColumn: "Id",
                keyValue: new Guid("a189e23d-8eb9-4b83-b6ed-61ec5d8b04bc"),
                columns: new[] { "ModelYear", "TrailerNumber" },
                values: new object[] { "2019", "507" });

            migrationBuilder.UpdateData(
                table: "Trailers",
                keyColumn: "Id",
                keyValue: new Guid("ae48c5b1-2c1f-4a7b-8cc1-4b89d7cc9f42"),
                columns: new[] { "ModelYear", "TrailerNumber" },
                values: new object[] { "2021", "500" });

            migrationBuilder.UpdateData(
                table: "Trailers",
                keyColumn: "Id",
                keyValue: new Guid("bfaad345-3a44-4d9a-b015-7f7b4f85d3a1"),
                columns: new[] { "ModelYear", "TrailerNumber" },
                values: new object[] { "2019", "502" });

            migrationBuilder.UpdateData(
                table: "Trailers",
                keyColumn: "Id",
                keyValue: new Guid("c3e8b097-6340-45b0-8eb0-9578f8409f52"),
                columns: new[] { "ModelYear", "TrailerNumber" },
                values: new object[] { "2020", "501" });

            migrationBuilder.UpdateData(
                table: "Trailers",
                keyColumn: "Id",
                keyValue: new Guid("cb49e342-d2b3-4b2b-9bb9-c2346d823dc0"),
                columns: new[] { "ModelYear", "TrailerNumber" },
                values: new object[] { "2022", "508" });

            migrationBuilder.UpdateData(
                table: "Trailers",
                keyColumn: "Id",
                keyValue: new Guid("d1b99cfa-cf73-4786-ae4a-4a49d1b179b5"),
                columns: new[] { "ModelYear", "TrailerNumber" },
                values: new object[] { "2020", "509" });

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("1c5a5bcf-0d38-4b2b-a8d2-b306a6d8b7da"),
                columns: new[] { "ModelYear", "TruckNumber" },
                values: new object[] { "2021", "101" });

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("5c1f054b-efef-4ee6-81cc-9f41e7c02ea2"),
                columns: new[] { "ModelYear", "TruckNumber" },
                values: new object[] { "2021", "105" });

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("bbbe9c6e-cda4-4b80-8988-e7456020dfe7"),
                columns: new[] { "ModelYear", "TruckNumber" },
                values: new object[] { "2020", "102" });

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("be8b8f36-0cfe-4509-98b5-2e779a9f8b07"),
                columns: new[] { "ModelYear", "TruckNumber" },
                values: new object[] { "2020", "106" });

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("bfa9f0a5-c189-4cc9-8e65-dfa218ec4f60"),
                columns: new[] { "ModelYear", "TruckNumber" },
                values: new object[] { "2020", "110" });

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("c303b365-bb6e-4a43-b473-61d25a9e39d3"),
                columns: new[] { "ModelYear", "TruckNumber" },
                values: new object[] { "2022", "109" });

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("cd0f5805-9409-420b-b6a3-740dfdba84a0"),
                columns: new[] { "ModelYear", "TruckNumber" },
                values: new object[] { "2021", "108" });

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("d1fda9c2-5a0a-4ca0-8a3d-b417c40d68c9"),
                columns: new[] { "ModelYear", "TruckNumber" },
                values: new object[] { "2019", "103" });

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("d833d70d-e77e-4e39-b700-0a743b2f1ed6"),
                columns: new[] { "ModelYear", "TruckNumber" },
                values: new object[] { "2022", "104" });

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("f4b963fd-1d56-4296-92a6-c92366a67bfc"),
                columns: new[] { "ModelYear", "TruckNumber" },
                values: new object[] { "2018", "107" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TruckNumber",
                table: "Trucks",
                type: "int",
                nullable: false,
                comment: "Truck number",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldComment: "Truck number");

            migrationBuilder.AlterColumn<int>(
                name: "ModelYear",
                table: "Trucks",
                type: "int",
                nullable: false,
                comment: "Truck produciton year",
                oldClrType: typeof(string),
                oldType: "nvarchar(4)",
                oldMaxLength: 4,
                oldComment: "Truck produciton year");

            migrationBuilder.AlterColumn<int>(
                name: "TrailerNumber",
                table: "Trailers",
                type: "int",
                nullable: false,
                comment: "Trailer number",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldComment: "Trailer number");

            migrationBuilder.AlterColumn<int>(
                name: "ModelYear",
                table: "Trailers",
                type: "int",
                nullable: false,
                comment: "Trailer production year",
                oldClrType: typeof(string),
                oldType: "nvarchar(4)",
                oldMaxLength: 4,
                oldComment: "Trailer production year");

            migrationBuilder.UpdateData(
                table: "Trailers",
                keyColumn: "Id",
                keyValue: new Guid("263fe327-e883-468e-83b3-ff072b38944a"),
                columns: new[] { "ModelYear", "TrailerNumber" },
                values: new object[] { 2020, 504 });

            migrationBuilder.UpdateData(
                table: "Trailers",
                keyColumn: "Id",
                keyValue: new Guid("47c3d1b7-9fa9-4ca6-9549-b9742fcf85cb"),
                columns: new[] { "ModelYear", "TrailerNumber" },
                values: new object[] { 2021, 506 });

            migrationBuilder.UpdateData(
                table: "Trailers",
                keyColumn: "Id",
                keyValue: new Guid("54830d70-bc69-4539-bd97-36d2093dfd0e"),
                columns: new[] { "ModelYear", "TrailerNumber" },
                values: new object[] { 2018, 505 });

            migrationBuilder.UpdateData(
                table: "Trailers",
                keyColumn: "Id",
                keyValue: new Guid("83274bc8-bef1-47ab-bbb1-b4422eae8d44"),
                columns: new[] { "ModelYear", "TrailerNumber" },
                values: new object[] { 2022, 503 });

            migrationBuilder.UpdateData(
                table: "Trailers",
                keyColumn: "Id",
                keyValue: new Guid("a189e23d-8eb9-4b83-b6ed-61ec5d8b04bc"),
                columns: new[] { "ModelYear", "TrailerNumber" },
                values: new object[] { 2019, 507 });

            migrationBuilder.UpdateData(
                table: "Trailers",
                keyColumn: "Id",
                keyValue: new Guid("ae48c5b1-2c1f-4a7b-8cc1-4b89d7cc9f42"),
                columns: new[] { "ModelYear", "TrailerNumber" },
                values: new object[] { 2021, 500 });

            migrationBuilder.UpdateData(
                table: "Trailers",
                keyColumn: "Id",
                keyValue: new Guid("bfaad345-3a44-4d9a-b015-7f7b4f85d3a1"),
                columns: new[] { "ModelYear", "TrailerNumber" },
                values: new object[] { 2019, 502 });

            migrationBuilder.UpdateData(
                table: "Trailers",
                keyColumn: "Id",
                keyValue: new Guid("c3e8b097-6340-45b0-8eb0-9578f8409f52"),
                columns: new[] { "ModelYear", "TrailerNumber" },
                values: new object[] { 2020, 501 });

            migrationBuilder.UpdateData(
                table: "Trailers",
                keyColumn: "Id",
                keyValue: new Guid("cb49e342-d2b3-4b2b-9bb9-c2346d823dc0"),
                columns: new[] { "ModelYear", "TrailerNumber" },
                values: new object[] { 2022, 508 });

            migrationBuilder.UpdateData(
                table: "Trailers",
                keyColumn: "Id",
                keyValue: new Guid("d1b99cfa-cf73-4786-ae4a-4a49d1b179b5"),
                columns: new[] { "ModelYear", "TrailerNumber" },
                values: new object[] { 2020, 509 });

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("1c5a5bcf-0d38-4b2b-a8d2-b306a6d8b7da"),
                columns: new[] { "ModelYear", "TruckNumber" },
                values: new object[] { 2021, 101 });

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("5c1f054b-efef-4ee6-81cc-9f41e7c02ea2"),
                columns: new[] { "ModelYear", "TruckNumber" },
                values: new object[] { 2021, 105 });

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("bbbe9c6e-cda4-4b80-8988-e7456020dfe7"),
                columns: new[] { "ModelYear", "TruckNumber" },
                values: new object[] { 2020, 102 });

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("be8b8f36-0cfe-4509-98b5-2e779a9f8b07"),
                columns: new[] { "ModelYear", "TruckNumber" },
                values: new object[] { 2020, 106 });

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("bfa9f0a5-c189-4cc9-8e65-dfa218ec4f60"),
                columns: new[] { "ModelYear", "TruckNumber" },
                values: new object[] { 2020, 110 });

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("c303b365-bb6e-4a43-b473-61d25a9e39d3"),
                columns: new[] { "ModelYear", "TruckNumber" },
                values: new object[] { 2022, 109 });

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("cd0f5805-9409-420b-b6a3-740dfdba84a0"),
                columns: new[] { "ModelYear", "TruckNumber" },
                values: new object[] { 2021, 108 });

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("d1fda9c2-5a0a-4ca0-8a3d-b417c40d68c9"),
                columns: new[] { "ModelYear", "TruckNumber" },
                values: new object[] { 2019, 103 });

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("d833d70d-e77e-4e39-b700-0a743b2f1ed6"),
                columns: new[] { "ModelYear", "TruckNumber" },
                values: new object[] { 2022, 104 });

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("f4b963fd-1d56-4296-92a6-c92366a67bfc"),
                columns: new[] { "ModelYear", "TruckNumber" },
                values: new object[] { 2018, 107 });
        }
    }
}
