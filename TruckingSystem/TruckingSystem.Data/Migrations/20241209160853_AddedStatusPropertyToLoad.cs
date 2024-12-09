using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckingSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedStatusPropertyToLoad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Loads");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Loads",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Shows the status of the load");

            migrationBuilder.UpdateData(
                table: "Loads",
                keyColumn: "Id",
                keyValue: new Guid("0f3a4d6f-8b2d-47a5-b8c7-f2d4e8f5b8a7"),
                column: "Status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Loads",
                keyColumn: "Id",
                keyValue: new Guid("3a9e2b56-957a-41a6-9bbd-b45d3f0d5e6f"),
                column: "Status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Loads",
                keyColumn: "Id",
                keyValue: new Guid("4b8c2d65-a3c5-4f7e-9943-ec2d3d48f88e"),
                column: "Status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Loads",
                keyColumn: "Id",
                keyValue: new Guid("5c3f2b57-9f4d-4d85-b6f2-e73e5d88c77d"),
                column: "Status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Loads",
                keyColumn: "Id",
                keyValue: new Guid("6a7d5c4f-b4a6-44c6-af7d-9f2c3f88d4a3"),
                column: "Status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Loads",
                keyColumn: "Id",
                keyValue: new Guid("7b8f2e6c-4c5f-4d66-9bbd-b47e3e88f22d"),
                column: "Status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Loads",
                keyColumn: "Id",
                keyValue: new Guid("8c2f3a4e-5f3a-4f98-a3b6-b38e7e4d5e6f"),
                column: "Status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Loads",
                keyColumn: "Id",
                keyValue: new Guid("9d6f5e3a-7d3b-45c7-b8e2-5f4a2f88e4f5"),
                column: "Status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Loads",
                keyColumn: "Id",
                keyValue: new Guid("e2e8fc12-5a4e-4d41-b8f2-c6ae5ea84600"),
                column: "Status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Loads",
                keyColumn: "Id",
                keyValue: new Guid("f1c9d2e4-8b8d-43a6-baf3-d48e51d4920a"),
                column: "Status",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Loads");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Loads",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Shows if the load is assigned or not");

            migrationBuilder.UpdateData(
                table: "Loads",
                keyColumn: "Id",
                keyValue: new Guid("0f3a4d6f-8b2d-47a5-b8c7-f2d4e8f5b8a7"),
                column: "IsAvailable",
                value: true);

            migrationBuilder.UpdateData(
                table: "Loads",
                keyColumn: "Id",
                keyValue: new Guid("3a9e2b56-957a-41a6-9bbd-b45d3f0d5e6f"),
                column: "IsAvailable",
                value: true);

            migrationBuilder.UpdateData(
                table: "Loads",
                keyColumn: "Id",
                keyValue: new Guid("4b8c2d65-a3c5-4f7e-9943-ec2d3d48f88e"),
                column: "IsAvailable",
                value: true);

            migrationBuilder.UpdateData(
                table: "Loads",
                keyColumn: "Id",
                keyValue: new Guid("5c3f2b57-9f4d-4d85-b6f2-e73e5d88c77d"),
                column: "IsAvailable",
                value: true);

            migrationBuilder.UpdateData(
                table: "Loads",
                keyColumn: "Id",
                keyValue: new Guid("6a7d5c4f-b4a6-44c6-af7d-9f2c3f88d4a3"),
                column: "IsAvailable",
                value: true);

            migrationBuilder.UpdateData(
                table: "Loads",
                keyColumn: "Id",
                keyValue: new Guid("7b8f2e6c-4c5f-4d66-9bbd-b47e3e88f22d"),
                column: "IsAvailable",
                value: true);

            migrationBuilder.UpdateData(
                table: "Loads",
                keyColumn: "Id",
                keyValue: new Guid("8c2f3a4e-5f3a-4f98-a3b6-b38e7e4d5e6f"),
                column: "IsAvailable",
                value: true);

            migrationBuilder.UpdateData(
                table: "Loads",
                keyColumn: "Id",
                keyValue: new Guid("9d6f5e3a-7d3b-45c7-b8e2-5f4a2f88e4f5"),
                column: "IsAvailable",
                value: true);

            migrationBuilder.UpdateData(
                table: "Loads",
                keyColumn: "Id",
                keyValue: new Guid("e2e8fc12-5a4e-4d41-b8f2-c6ae5ea84600"),
                column: "IsAvailable",
                value: true);

            migrationBuilder.UpdateData(
                table: "Loads",
                keyColumn: "Id",
                keyValue: new Guid("f1c9d2e4-8b8d-43a6-baf3-d48e51d4920a"),
                column: "IsAvailable",
                value: true);
        }
    }
}
