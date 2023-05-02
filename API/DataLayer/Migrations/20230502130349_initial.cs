using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryImage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    CityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityImage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.CityId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Entertainments",
                columns: table => new
                {
                    EntertainmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntertainmentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntertainmentDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entertainments", x => x.EntertainmentId);
                    table.ForeignKey(
                        name: "FK_Entertainments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "CategoryEntityEntertainmentItemEntity",
                columns: table => new
                {
                    CategoriesCategoryId = table.Column<int>(type: "int", nullable: false),
                    EntertainmentsEntertainmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryEntityEntertainmentItemEntity", x => new { x.CategoriesCategoryId, x.EntertainmentsEntertainmentId });
                    table.ForeignKey(
                        name: "FK_CategoryEntityEntertainmentItemEntity_Categories_CategoriesCategoryId",
                        column: x => x.CategoriesCategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryEntityEntertainmentItemEntity_Entertainments_EntertainmentsEntertainmentId",
                        column: x => x.EntertainmentsEntertainmentId,
                        principalTable: "Entertainments",
                        principalColumn: "EntertainmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CityEntityEntertainmentItemEntity",
                columns: table => new
                {
                    CitiesCityId = table.Column<int>(type: "int", nullable: false),
                    EntertainmentsEntertainmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityEntityEntertainmentItemEntity", x => new { x.CitiesCityId, x.EntertainmentsEntertainmentId });
                    table.ForeignKey(
                        name: "FK_CityEntityEntertainmentItemEntity_Cities_CitiesCityId",
                        column: x => x.CitiesCityId,
                        principalTable: "Cities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CityEntityEntertainmentItemEntity_Entertainments_EntertainmentsEntertainmentId",
                        column: x => x.EntertainmentsEntertainmentId,
                        principalTable: "Entertainments",
                        principalColumn: "EntertainmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gallery",
                columns: table => new
                {
                    ImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntertainmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gallery", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_Gallery_Entertainments_EntertainmentId",
                        column: x => x.EntertainmentId,
                        principalTable: "Entertainments",
                        principalColumn: "EntertainmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ReservationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntertainmentId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BreakTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxCount = table.Column<int>(type: "int", nullable: false),
                    PeriodTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReservationTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ReservationId);
                    table.ForeignKey(
                        name: "FK_Reservations_Entertainments_EntertainmentId",
                        column: x => x.EntertainmentId,
                        principalTable: "Entertainments",
                        principalColumn: "EntertainmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    Review = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntertainmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Reviews_Entertainments_EntertainmentId",
                        column: x => x.EntertainmentId,
                        principalTable: "Entertainments",
                        principalColumn: "EntertainmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryEntityEntertainmentItemEntity_EntertainmentsEntertainmentId",
                table: "CategoryEntityEntertainmentItemEntity",
                column: "EntertainmentsEntertainmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CityEntityEntertainmentItemEntity_EntertainmentsEntertainmentId",
                table: "CityEntityEntertainmentItemEntity",
                column: "EntertainmentsEntertainmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Entertainments_UserId",
                table: "Entertainments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Gallery_EntertainmentId",
                table: "Gallery",
                column: "EntertainmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_EntertainmentId",
                table: "Reservations",
                column: "EntertainmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserId",
                table: "Reservations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_EntertainmentId",
                table: "Reviews",
                column: "EntertainmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_StateId",
                table: "Users",
                column: "StateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryEntityEntertainmentItemEntity");

            migrationBuilder.DropTable(
                name: "CityEntityEntertainmentItemEntity");

            migrationBuilder.DropTable(
                name: "Gallery");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Entertainments");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "States");
        }
    }
}
