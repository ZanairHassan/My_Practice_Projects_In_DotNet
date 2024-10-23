using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuditApp.Migrations
{
    /// <inheritdoc />
    public partial class AddMonthYearModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MonthId",
                table: "Softwares",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YearId",
                table: "Softwares",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MonthId",
                table: "MobileCompanies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YearId",
                table: "MobileCompanies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MonthId",
                table: "Insurances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YearId",
                table: "Insurances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MonthId",
                table: "Incomes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YearId",
                table: "Incomes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MonthId",
                table: "Funds",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YearId",
                table: "Funds",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MonthId",
                table: "Expenses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YearId",
                table: "Expenses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MonthId",
                table: "Softwares");

            migrationBuilder.DropColumn(
                name: "YearId",
                table: "Softwares");

            migrationBuilder.DropColumn(
                name: "MonthId",
                table: "MobileCompanies");

            migrationBuilder.DropColumn(
                name: "YearId",
                table: "MobileCompanies");

            migrationBuilder.DropColumn(
                name: "MonthId",
                table: "Insurances");

            migrationBuilder.DropColumn(
                name: "YearId",
                table: "Insurances");

            migrationBuilder.DropColumn(
                name: "MonthId",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "YearId",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "MonthId",
                table: "Funds");

            migrationBuilder.DropColumn(
                name: "YearId",
                table: "Funds");

            migrationBuilder.DropColumn(
                name: "MonthId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "YearId",
                table: "Expenses");
        }
    }
}
