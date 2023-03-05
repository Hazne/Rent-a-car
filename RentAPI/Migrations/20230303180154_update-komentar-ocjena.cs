using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentAPI.Migrations
{
    public partial class updatekomentarocjena : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RezervisanjeId",
                table: "Ocjenas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RezervisanjeId",
                table: "Komentars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ocjenas_RezervisanjeId",
                table: "Ocjenas",
                column: "RezervisanjeId");

            migrationBuilder.CreateIndex(
                name: "IX_Komentars_RezervisanjeId",
                table: "Komentars",
                column: "RezervisanjeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Komentars_Rezervisanjes_RezervisanjeId",
                table: "Komentars",
                column: "RezervisanjeId",
                principalTable: "Rezervisanjes",
                principalColumn: "RezervisanjeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ocjenas_Rezervisanjes_RezervisanjeId",
                table: "Ocjenas",
                column: "RezervisanjeId",
                principalTable: "Rezervisanjes",
                principalColumn: "RezervisanjeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Komentars_Rezervisanjes_RezervisanjeId",
                table: "Komentars");

            migrationBuilder.DropForeignKey(
                name: "FK_Ocjenas_Rezervisanjes_RezervisanjeId",
                table: "Ocjenas");

            migrationBuilder.DropIndex(
                name: "IX_Ocjenas_RezervisanjeId",
                table: "Ocjenas");

            migrationBuilder.DropIndex(
                name: "IX_Komentars_RezervisanjeId",
                table: "Komentars");

            migrationBuilder.DropColumn(
                name: "RezervisanjeId",
                table: "Ocjenas");

            migrationBuilder.DropColumn(
                name: "RezervisanjeId",
                table: "Komentars");
        }
    }
}
