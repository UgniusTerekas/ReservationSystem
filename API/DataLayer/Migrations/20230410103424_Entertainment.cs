using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class Entertainment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EntertainmentItemEntityEntertainmentId",
                table: "Cities",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EntertainmentItemEntityEntertainmentId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Entertainments",
                columns: table => new
                {
                    EntertainmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntertainmentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntertainmentDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entertainments", x => x.EntertainmentId);
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
                name: "Reviews",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    Review = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntertainmentItemEntityEntertainmentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Reviews_Entertainments_EntertainmentItemEntityEntertainmentId",
                        column: x => x.EntertainmentItemEntityEntertainmentId,
                        principalTable: "Entertainments",
                        principalColumn: "EntertainmentId");
                    table.ForeignKey(
                        name: "FK_Reviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_EntertainmentItemEntityEntertainmentId",
                table: "Cities",
                column: "EntertainmentItemEntityEntertainmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_EntertainmentItemEntityEntertainmentId",
                table: "Categories",
                column: "EntertainmentItemEntityEntertainmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Gallery_EntertainmentId",
                table: "Gallery",
                column: "EntertainmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_EntertainmentItemEntityEntertainmentId",
                table: "Reviews",
                column: "EntertainmentItemEntityEntertainmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Entertainments_EntertainmentItemEntityEntertainmentId",
                table: "Categories",
                column: "EntertainmentItemEntityEntertainmentId",
                principalTable: "Entertainments",
                principalColumn: "EntertainmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Entertainments_EntertainmentItemEntityEntertainmentId",
                table: "Cities",
                column: "EntertainmentItemEntityEntertainmentId",
                principalTable: "Entertainments",
                principalColumn: "EntertainmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Entertainments_EntertainmentItemEntityEntertainmentId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Entertainments_EntertainmentItemEntityEntertainmentId",
                table: "Cities");

            migrationBuilder.DropTable(
                name: "Gallery");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Entertainments");

            migrationBuilder.DropIndex(
                name: "IX_Cities_EntertainmentItemEntityEntertainmentId",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Categories_EntertainmentItemEntityEntertainmentId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "EntertainmentItemEntityEntertainmentId",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "EntertainmentItemEntityEntertainmentId",
                table: "Categories");
        }
    }
}
