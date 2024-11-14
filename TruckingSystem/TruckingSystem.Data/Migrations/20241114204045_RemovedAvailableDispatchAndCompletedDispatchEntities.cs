using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckingSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovedAvailableDispatchAndCompletedDispatchEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dispatches_AvailableDispatches_AvailableDispatchId",
                table: "Dispatches");

            migrationBuilder.DropForeignKey(
                name: "FK_Dispatches_CompletedDispatches_CompletedDispatchId",
                table: "Dispatches");

            migrationBuilder.DropTable(
                name: "AvailableDispatches");

            migrationBuilder.DropTable(
                name: "CompletedDispatches");

            migrationBuilder.DropIndex(
                name: "IX_Dispatches_AvailableDispatchId",
                table: "Dispatches");

            migrationBuilder.DropIndex(
                name: "IX_Dispatches_CompletedDispatchId",
                table: "Dispatches");

            migrationBuilder.DropColumn(
                name: "AvailableDispatchId",
                table: "Dispatches");

            migrationBuilder.DropColumn(
                name: "CompletedDispatchId",
                table: "Dispatches");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AvailableDispatchId",
                table: "Dispatches",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CompletedDispatchId",
                table: "Dispatches",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AvailableDispatches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Available dispatch identifier")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailableDispatches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompletedDispatches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Complete dispatch identifier")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompletedDispatches", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dispatches_AvailableDispatchId",
                table: "Dispatches",
                column: "AvailableDispatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Dispatches_CompletedDispatchId",
                table: "Dispatches",
                column: "CompletedDispatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dispatches_AvailableDispatches_AvailableDispatchId",
                table: "Dispatches",
                column: "AvailableDispatchId",
                principalTable: "AvailableDispatches",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Dispatches_CompletedDispatches_CompletedDispatchId",
                table: "Dispatches",
                column: "CompletedDispatchId",
                principalTable: "CompletedDispatches",
                principalColumn: "Id");
        }
    }
}
