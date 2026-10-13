using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TerraZooApp.Migrations
{
    /// <inheritdoc />
    public partial class migrationnew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Tour_id_tour",
                table: "Ticket");

            migrationBuilder.AlterColumn<int>(
                name: "id_tour",
                table: "Ticket",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Tour_id_tour",
                table: "Ticket",
                column: "id_tour",
                principalTable: "Tour",
                principalColumn: "id_tour");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Tour_id_tour",
                table: "Ticket");

            migrationBuilder.AlterColumn<int>(
                name: "id_tour",
                table: "Ticket",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Tour_id_tour",
                table: "Ticket",
                column: "id_tour",
                principalTable: "Tour",
                principalColumn: "id_tour",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
