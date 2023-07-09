using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CardsCustomers.Migrations
{
    /// <inheritdoc />
    public partial class CreationTblRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "Deleted",
            //    table: "PointsPerMoney");

            //migrationBuilder.DropColumn(
            //    name: "IdUserAdminInsert",
            //    table: "PointsPerMoney");

            //migrationBuilder.DropColumn(
            //    name: "Deleted",
            //    table: "Discount");

            //migrationBuilder.DropColumn(
            //    name: "IdUserAdminInsert",
            //    table: "Discount");

            //migrationBuilder.AddColumn<int>(
            //    name: "IdUserRole",
            //    table: "UserAdmin",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.AddColumn<int>(
            //    name: "PointsFromCurTrans",
            //    table: "Transaction",
            //    type: "int",
            //    nullable: true);

            //migrationBuilder.AlterColumn<int>(
            //    name: "Points",
            //    table: "Customer",
            //    type: "int",
            //    nullable: true,
            //    oldClrType: typeof(int),
            //    oldType: "int");

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    IdUserRole = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.IdUserRole);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "UserRole");

            //migrationBuilder.DropColumn(
            //    name: "IdUserRole",
            //    table: "UserAdmin");

            //migrationBuilder.DropColumn(
            //    name: "PointsFromCurTrans",
            //    table: "Transaction");

            //migrationBuilder.AddColumn<bool>(
            //    name: "Deleted",
            //    table: "PointsPerMoney",
            //    type: "bit",
            //    nullable: false,
            //    defaultValue: false);

            //migrationBuilder.AddColumn<int>(
            //    name: "IdUserAdminInsert",
            //    table: "PointsPerMoney",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.AddColumn<bool>(
            //    name: "Deleted",
            //    table: "Discount",
            //    type: "bit",
            //    nullable: false,
            //    defaultValue: false);

            //migrationBuilder.AddColumn<int>(
            //    name: "IdUserAdminInsert",
            //    table: "Discount",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Points",
                table: "Customer",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
