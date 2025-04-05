using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema_webapp.Migrations
{
    /// <inheritdoc />
    public partial class _190105042025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HallId",
                table: "Showtimes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Showtimes_HallId",
                table: "Showtimes",
                column: "HallId");

            migrationBuilder.AddForeignKey(
                name: "FK_Showtimes_Halls_HallId",
                table: "Showtimes",
                column: "HallId",
                principalTable: "Halls",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Showtimes_Halls_HallId",
                table: "Showtimes");

            migrationBuilder.DropIndex(
                name: "IX_Showtimes_HallId",
                table: "Showtimes");

            migrationBuilder.DropColumn(
                name: "HallId",
                table: "Showtimes");
        }
    }
}
