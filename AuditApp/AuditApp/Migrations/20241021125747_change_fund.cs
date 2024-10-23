using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuditApp.Migrations
{
    /// <inheritdoc />
    public partial class change_fund : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FundTypeID",
                table: "Funds");

            migrationBuilder.AlterColumn<int>(
                name: "FundTypeUpdatedBy",
                table: "Funds",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FundTypeDeletedBy",
                table: "Funds",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FundTypeCreatedBy",
                table: "Funds",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "FundName",
                table: "Funds",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FundTypeName",
                table: "Funds",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FundName",
                table: "Funds");

            migrationBuilder.DropColumn(
                name: "FundTypeName",
                table: "Funds");

            migrationBuilder.AlterColumn<int>(
                name: "FundTypeUpdatedBy",
                table: "Funds",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FundTypeDeletedBy",
                table: "Funds",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FundTypeCreatedBy",
                table: "Funds",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FundTypeID",
                table: "Funds",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
