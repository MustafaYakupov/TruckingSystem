using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckingSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedLoadTableToNotRequireDriver : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loads_Drivers_DriverId",
                table: "Loads");

            migrationBuilder.AlterColumn<Guid>(
                name: "DriverId",
                table: "Loads",
                type: "uniqueidentifier",
                nullable: true,
                comment: "Driver identifier",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "Driver identifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Loads_Drivers_DriverId",
                table: "Loads",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loads_Drivers_DriverId",
                table: "Loads");

            migrationBuilder.AlterColumn<Guid>(
                name: "DriverId",
                table: "Loads",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "Driver identifier",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldComment: "Driver identifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Loads_Drivers_DriverId",
                table: "Loads",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
