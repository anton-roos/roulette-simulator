using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace roulette_simulator.Migrations
{
    /// <inheritdoc />
    public partial class AddBalanceToSpin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Balance",
                table: "Spins",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Spins");
        }
    }
}
