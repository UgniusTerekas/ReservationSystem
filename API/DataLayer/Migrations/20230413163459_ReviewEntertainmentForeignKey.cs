using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class ReviewEntertainmentForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Entertainments_EntertainmentItemEntityEntertainmentId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_EntertainmentItemEntityEntertainmentId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "EntertainmentItemEntityEntertainmentId",
                table: "Reviews");

            migrationBuilder.AddColumn<int>(
                name: "EntertainmentId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_EntertainmentId",
                table: "Reviews",
                column: "EntertainmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Entertainments_EntertainmentId",
                table: "Reviews",
                column: "EntertainmentId",
                principalTable: "Entertainments",
                principalColumn: "EntertainmentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Entertainments_EntertainmentId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_EntertainmentId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "EntertainmentId",
                table: "Reviews");

            migrationBuilder.AddColumn<int>(
                name: "EntertainmentItemEntityEntertainmentId",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_EntertainmentItemEntityEntertainmentId",
                table: "Reviews",
                column: "EntertainmentItemEntityEntertainmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Entertainments_EntertainmentItemEntityEntertainmentId",
                table: "Reviews",
                column: "EntertainmentItemEntityEntertainmentId",
                principalTable: "Entertainments",
                principalColumn: "EntertainmentId");
        }
    }
}
