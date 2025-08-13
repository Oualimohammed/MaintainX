using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pri.Ek2.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddAttachmentPathsJsonToMaintenanceLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttachmentPaths",
                table: "MaintenanceLogs");

            migrationBuilder.AddColumn<string>(
                name: "AttachmentPathsJson",
                table: "MaintenanceLogs",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttachmentPathsJson",
                table: "MaintenanceLogs");

            migrationBuilder.AddColumn<string>(
                name: "AttachmentPaths",
                table: "MaintenanceLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
