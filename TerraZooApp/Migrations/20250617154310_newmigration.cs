using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TerraZooApp.Migrations
{
    /// <inheritdoc />
    public partial class newmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "experience_years",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "foreign_languages",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "status",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "experience_years",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "foreign_languages",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "Users",
                type: "text",
                nullable: true);
        }
    }
}
