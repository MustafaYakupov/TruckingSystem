using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckingSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedLicencePlateNumberLength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LicensePlate",
                table: "Trucks",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "Truck license plate",
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3,
                oldComment: "Truck license plate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LicensePlate",
                table: "Trucks",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                comment: "Truck license plate",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "Truck license plate");
        }
    }
}
