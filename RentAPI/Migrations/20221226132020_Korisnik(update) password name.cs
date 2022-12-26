using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentAPI.Migrations
{
    public partial class Korisnikupdatepasswordname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModelAutomobilas_Proizvodjacs_ProizvodjacId",
                table: "ModelAutomobilas");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Korisniks");

            migrationBuilder.RenameColumn(
                name: "PasswordSalt",
                table: "Korisniks",
                newName: "Password");

            migrationBuilder.AlterColumn<int>(
                name: "ProizvodjacId",
                table: "ModelAutomobilas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ModelAutomobilas_Proizvodjacs_ProizvodjacId",
                table: "ModelAutomobilas",
                column: "ProizvodjacId",
                principalTable: "Proizvodjacs",
                principalColumn: "ProizvodjacId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModelAutomobilas_Proizvodjacs_ProizvodjacId",
                table: "ModelAutomobilas");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Korisniks",
                newName: "PasswordSalt");

            migrationBuilder.AlterColumn<int>(
                name: "ProizvodjacId",
                table: "ModelAutomobilas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Korisniks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ModelAutomobilas_Proizvodjacs_ProizvodjacId",
                table: "ModelAutomobilas",
                column: "ProizvodjacId",
                principalTable: "Proizvodjacs",
                principalColumn: "ProizvodjacId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
