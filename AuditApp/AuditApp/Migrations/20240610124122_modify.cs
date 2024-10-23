using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuditApp.Migrations
{
    /// <inheritdoc />
    public partial class modify : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfigurationExpiry",
                table: "Configurations");

            migrationBuilder.RenameColumn(
                name: "RequestTime",
                table: "Requests",
                newName: "LastRequestTime");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Requests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "LastRequestTime",
                table: "Requests",
                newName: "RequestTime");

            migrationBuilder.AddColumn<string>(
                name: "ConfigurationExpiry",
                table: "Configurations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
