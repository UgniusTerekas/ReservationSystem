using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UserRelationsEntertainmentReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Entertainments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Entertainments_UserId",
                table: "Entertainments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Entertainments_Users_UserId",
                table: "Entertainments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entertainments_Users_UserId",
                table: "Entertainments");

            migrationBuilder.DropIndex(
                name: "IX_Entertainments_UserId",
                table: "Entertainments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Entertainments");
        }
    }
}
