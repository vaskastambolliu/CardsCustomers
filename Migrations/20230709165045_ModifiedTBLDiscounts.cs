using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CardsCustomers.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedTBLDiscounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdUserAdmin",
                table: "Discount",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdUserAdmin",
                table: "Discount");
        }
    }
}
