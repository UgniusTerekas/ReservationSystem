using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class CityCategoryEntertainments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Entertainments_EntertainmentItemEntityEntertainmentId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Entertainments_EntertainmentItemEntityEntertainmentId",
                table: "Cities");

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

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Entertainments",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

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

            migrationBuilder.CreateIndex(
                name: "IX_CategoryEntityEntertainmentItemEntity_EntertainmentsEntertainmentId",
                table: "CategoryEntityEntertainmentItemEntity",
                column: "EntertainmentsEntertainmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CityEntityEntertainmentItemEntity_EntertainmentsEntertainmentId",
                table: "CityEntityEntertainmentItemEntity",
                column: "EntertainmentsEntertainmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryEntityEntertainmentItemEntity");

            migrationBuilder.DropTable(
                name: "CityEntityEntertainmentItemEntity");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Entertainments",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

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

            migrationBuilder.CreateIndex(
                name: "IX_Cities_EntertainmentItemEntityEntertainmentId",
                table: "Cities",
                column: "EntertainmentItemEntityEntertainmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_EntertainmentItemEntityEntertainmentId",
                table: "Categories",
                column: "EntertainmentItemEntityEntertainmentId");

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
    }
}
