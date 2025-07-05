using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pri.Ek2.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldEmailAndFirstAndLastNameToEntitie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "UserProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "UserProfiles");
        }
    }
}
