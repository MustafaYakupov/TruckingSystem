using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckingSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

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
                name: "BrokerCompanies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Unique Identifier"),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Broker company name"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Shows weather Broker Company is deleted or not")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrokerCompanies", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "DriverManagers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Unique Identifier"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Driver manager first name"),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Driver manager last name"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverManagers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Unique Identifier"),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Part type"),
                    Make = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Part make"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Shows weather part is deleted or not")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trailers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Unique Identifier"),
                    Make = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Trailer make"),
                    Type = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, comment: "Trailer type"),
                    ModelYear = table.Column<int>(type: "int", nullable: false, comment: "Trailer production year"),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false, comment: "Shows weather trailer is available or not"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Shows weather trailer is deleted or not")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trailers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BrokerCompanyDriverManager",
                columns: table => new
                {
                    BrokerCompaniesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DriverManagersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrokerCompanyDriverManager", x => new { x.BrokerCompaniesId, x.DriverManagersId });
                    table.ForeignKey(
                        name: "FK_BrokerCompanyDriverManager_BrokerCompanies_BrokerCompaniesId",
                        column: x => x.BrokerCompaniesId,
                        principalTable: "BrokerCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BrokerCompanyDriverManager_DriverManagers_DriverManagersId",
                        column: x => x.DriverManagersId,
                        principalTable: "DriverManagers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trucks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Unique Identifier"),
                    TruckNumber = table.Column<int>(type: "int", nullable: false, comment: "Truck number"),
                    Make = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Truck make"),
                    Model = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Truck model"),
                    LicensePlate = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false, comment: "Truck license plate"),
                    ModelYear = table.Column<int>(type: "int", nullable: false, comment: "Truck produciton year"),
                    Color = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "Truck color"),
                    TrailerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Trailer identifier"),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false, comment: "Shows weather truck is available or not"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Shows weather truck is deleted or not")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trucks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trucks_Trailers_TrailerId",
                        column: x => x.TrailerId,
                        principalTable: "Trailers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Unique Identifier"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Driver first name"),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Driver last name"),
                    LicenseNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "Driver license number"),
                    TruckId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "The Identifier of the driver's truck"),
                    TrailerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "The Identifier of the driver's trailer"),
                    DriverManagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "The Identifier of the driver's Manager"),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false, comment: "Shows weather the driver is busy or available"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Shows weather driver is deleted or not")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drivers_DriverManagers_DriverManagerId",
                        column: x => x.DriverManagerId,
                        principalTable: "DriverManagers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Drivers_Trailers_TrailerId",
                        column: x => x.TrailerId,
                        principalTable: "Trailers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Drivers_Trucks_TruckId",
                        column: x => x.TruckId,
                        principalTable: "Trucks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TrucksParts",
                columns: table => new
                {
                    TruckId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Game identifier"),
                    PartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Part identifier")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrucksParts", x => new { x.TruckId, x.PartId });
                    table.ForeignKey(
                        name: "FK_TrucksParts_Parts_PartId",
                        column: x => x.PartId,
                        principalTable: "Parts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrucksParts_Trucks_TruckId",
                        column: x => x.TruckId,
                        principalTable: "Trucks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Loads",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Unique Identifier"),
                    PickupLocation = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "The address where the load is shipping from"),
                    DeliveryLocation = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "The address where the load is going to"),
                    Weight = table.Column<int>(type: "int", nullable: false, comment: "Weight of the product"),
                    Temperature = table.Column<double>(type: "float", nullable: true, comment: "Temperature the product must be kept at"),
                    PickupTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Pick up appointment time"),
                    DeliveryTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Delivery appointment time"),
                    Distance = table.Column<int>(type: "int", nullable: false, comment: "Load distance"),
                    DriverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Driver identifier"),
                    BrokerCompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "BrokerCompany identifier"),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false, comment: "Shows if the load is assigned or not"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Shows weather load is deleted or not")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Loads_BrokerCompanies_BrokerCompanyId",
                        column: x => x.BrokerCompanyId,
                        principalTable: "BrokerCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Loads_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Dispatches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Unique Identifier"),
                    DriverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Unique Identifier"),
                    TruckId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Truck Unique Identifier"),
                    TrailerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Trailer Unique Identifier"),
                    DriverManagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "DriverManager Unique Identifier"),
                    LoadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Load Unique Identifier"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Shows weather dispatch is deleted or not"),
                    AvailableDispatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompletedDispatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dispatches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dispatches_AvailableDispatches_AvailableDispatchId",
                        column: x => x.AvailableDispatchId,
                        principalTable: "AvailableDispatches",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Dispatches_CompletedDispatches_CompletedDispatchId",
                        column: x => x.CompletedDispatchId,
                        principalTable: "CompletedDispatches",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Dispatches_DriverManagers_DriverManagerId",
                        column: x => x.DriverManagerId,
                        principalTable: "DriverManagers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Dispatches_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Dispatches_Loads_LoadId",
                        column: x => x.LoadId,
                        principalTable: "Loads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Dispatches_Trailers_TrailerId",
                        column: x => x.TrailerId,
                        principalTable: "Trailers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Dispatches_Trucks_TruckId",
                        column: x => x.TruckId,
                        principalTable: "Trucks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BrokerCompanyDriverManager_DriverManagersId",
                table: "BrokerCompanyDriverManager",
                column: "DriverManagersId");

            migrationBuilder.CreateIndex(
                name: "IX_Dispatches_AvailableDispatchId",
                table: "Dispatches",
                column: "AvailableDispatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Dispatches_CompletedDispatchId",
                table: "Dispatches",
                column: "CompletedDispatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Dispatches_DriverId",
                table: "Dispatches",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Dispatches_DriverManagerId",
                table: "Dispatches",
                column: "DriverManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Dispatches_LoadId",
                table: "Dispatches",
                column: "LoadId");

            migrationBuilder.CreateIndex(
                name: "IX_Dispatches_TrailerId",
                table: "Dispatches",
                column: "TrailerId");

            migrationBuilder.CreateIndex(
                name: "IX_Dispatches_TruckId",
                table: "Dispatches",
                column: "TruckId");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_DriverManagerId",
                table: "Drivers",
                column: "DriverManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_TrailerId",
                table: "Drivers",
                column: "TrailerId");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_TruckId",
                table: "Drivers",
                column: "TruckId",
                unique: true,
                filter: "[TruckId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Loads_BrokerCompanyId",
                table: "Loads",
                column: "BrokerCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Loads_DriverId",
                table: "Loads",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Trucks_TrailerId",
                table: "Trucks",
                column: "TrailerId",
                unique: true,
                filter: "[TrailerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TrucksParts_PartId",
                table: "TrucksParts",
                column: "PartId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BrokerCompanyDriverManager");

            migrationBuilder.DropTable(
                name: "Dispatches");

            migrationBuilder.DropTable(
                name: "TrucksParts");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "AvailableDispatches");

            migrationBuilder.DropTable(
                name: "CompletedDispatches");

            migrationBuilder.DropTable(
                name: "Loads");

            migrationBuilder.DropTable(
                name: "Parts");

            migrationBuilder.DropTable(
                name: "BrokerCompanies");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "DriverManagers");

            migrationBuilder.DropTable(
                name: "Trucks");

            migrationBuilder.DropTable(
                name: "Trailers");
        }
    }
}
