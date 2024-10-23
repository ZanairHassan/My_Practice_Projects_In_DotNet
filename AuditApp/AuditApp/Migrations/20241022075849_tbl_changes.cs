using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuditApp.Migrations
{
    /// <inheritdoc />
    public partial class tbl_changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "InvestmentTypes");

            migrationBuilder.RenameColumn(
                name: "InvestmentTypeIsDeleted",
                table: "InvestmentTypes",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "InvestmentcreatedBy",
                table: "Investments",
                newName: "InvestmentCreatedBy");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Investments",
                newName: "InvestmentDeletedBy");

            migrationBuilder.RenameColumn(
                name: "InvestmentDeleted",
                table: "Investments",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "IncomeTypeID",
                table: "Incomes",
                newName: "IncomeTypeName");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedTime",
                table: "InvestmentTypes",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "InvestmentTypeUpdatedBy",
                table: "InvestmentTypes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "InvestmentTypeIsCreatedBy",
                table: "InvestmentTypes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "InvestmentTypes",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "InvestmentTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InvestmentTypeDeletedBy",
                table: "InvestmentTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedTime",
                table: "Investments",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Investments",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Investments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IncomeUpdatedBy",
                table: "Incomes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "IncomeDeletedBy",
                table: "Incomes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "IncomeCreatedBy",
                table: "Incomes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "IncomeDetails",
                table: "Incomes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "InvestmentTypes");

            migrationBuilder.DropColumn(
                name: "InvestmentTypeDeletedBy",
                table: "InvestmentTypes");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Investments");

            migrationBuilder.DropColumn(
                name: "IncomeDetails",
                table: "Incomes");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "InvestmentTypes",
                newName: "InvestmentTypeIsDeleted");

            migrationBuilder.RenameColumn(
                name: "InvestmentCreatedBy",
                table: "Investments",
                newName: "InvestmentcreatedBy");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Investments",
                newName: "InvestmentDeleted");

            migrationBuilder.RenameColumn(
                name: "InvestmentDeletedBy",
                table: "Investments",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "IncomeTypeName",
                table: "Incomes",
                newName: "IncomeTypeID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedTime",
                table: "InvestmentTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InvestmentTypeUpdatedBy",
                table: "InvestmentTypes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InvestmentTypeIsCreatedBy",
                table: "InvestmentTypes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "InvestmentTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "InvestmentTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedTime",
                table: "Investments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Investments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IncomeUpdatedBy",
                table: "Incomes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IncomeDeletedBy",
                table: "Incomes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IncomeCreatedBy",
                table: "Incomes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
