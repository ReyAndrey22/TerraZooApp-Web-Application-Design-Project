using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TerraZooApp.Migrations
{
    /// <inheritdoc />
    public partial class migrationsecond : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Tour_id_tour",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Review_id_tour",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "id_tour",
                table: "Review");

            migrationBuilder.AddColumn<int>(
                name: "id_animal",
                table: "Review",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Review_id_animal",
                table: "Review",
                column: "id_animal");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Animal_id_animal",
                table: "Review",
                column: "id_animal",
                principalTable: "Animal",
                principalColumn: "id_animal");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Animal_id_animal",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Review_id_animal",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "id_animal",
                table: "Review");

            migrationBuilder.AddColumn<int>(
                name: "id_tour",
                table: "Review",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Review_id_tour",
                table: "Review",
                column: "id_tour");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Tour_id_tour",
                table: "Review",
                column: "id_tour",
                principalTable: "Tour",
                principalColumn: "id_tour",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
