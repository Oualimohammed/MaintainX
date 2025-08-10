using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pri.Ek2.Core.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMaintenanceLogEntitie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AttachmentPaths",
                table: "MaintenanceLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttachmentPaths",
                table: "MaintenanceLogs");
        }
    }
}
