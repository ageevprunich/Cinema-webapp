using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema_webapp.Migrations
{
    /// <inheritdoc />
    public partial class _214006042025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Seats");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Seats",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
