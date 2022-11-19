using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentAPI.Migrations
{
    public partial class Tabeleiveze : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Grads",
                columns: table => new
                {
                    GradId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImeGrada = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostanskiKod = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grads", x => x.GradId);
                });

            migrationBuilder.CreateTable(
                name: "Proizvodjacs",
                columns: table => new
                {
                    ProizvodjacId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImeProizvodjaca = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proizvodjacs", x => x.ProizvodjacId);
                });

            migrationBuilder.CreateTable(
                name: "TipAutomobilas",
                columns: table => new
                {
                    TipAutomobilaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImeTipa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipAutomobilas", x => x.TipAutomobilaId);
                });

            migrationBuilder.CreateTable(
                name: "TipGorivas",
                columns: table => new
                {
                    TipGorivaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImeGoriva = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipGorivas", x => x.TipGorivaId);
                });

            migrationBuilder.CreateTable(
                name: "TipKorisnikas",
                columns: table => new
                {
                    TipKorisnikaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tip = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipKorisnikas", x => x.TipKorisnikaId);
                });

            migrationBuilder.CreateTable(
                name: "Izdavacs",
                columns: table => new
                {
                    IzdavacId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImeIzdavaca = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojMobitela = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VrijemeOtvaranja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VrijemeZatvaranja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GradId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Izdavacs", x => x.IzdavacId);
                    table.ForeignKey(
                        name: "FK_Izdavacs_Grads_GradId",
                        column: x => x.GradId,
                        principalTable: "Grads",
                        principalColumn: "GradId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModelAutomobilas",
                columns: table => new
                {
                    ModelAutomobilaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImeModela = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProizvodjacId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelAutomobilas", x => x.ModelAutomobilaId);
                    table.ForeignKey(
                        name: "FK_ModelAutomobilas_Proizvodjacs_ProizvodjacId",
                        column: x => x.ProizvodjacId,
                        principalTable: "Proizvodjacs",
                        principalColumn: "ProizvodjacId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Korisniks",
                columns: table => new
                {
                    KorisnikId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojMobitela = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordSalt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GradId = table.Column<int>(type: "int", nullable: false),
                    TipKorisnikaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisniks", x => x.KorisnikId);
                    table.ForeignKey(
                        name: "FK_Korisniks_Grads_GradId",
                        column: x => x.GradId,
                        principalTable: "Grads",
                        principalColumn: "GradId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Korisniks_TipKorisnikas_TipKorisnikaId",
                        column: x => x.TipKorisnikaId,
                        principalTable: "TipKorisnikas",
                        principalColumn: "TipKorisnikaId");
                });

            migrationBuilder.CreateTable(
                name: "Automobils",
                columns: table => new
                {
                    AutomobilId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tablice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slika = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    BrojAutomobila = table.Column<int>(type: "int", nullable: false),
                    CijenaPoDanu = table.Column<double>(type: "float", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatumProizvodnje = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Kolometraza = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vuca = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojSjedala = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    IzdavacId = table.Column<int>(type: "int", nullable: false),
                    TipGorivaId = table.Column<int>(type: "int", nullable: false),
                    TipAutomobilaId = table.Column<int>(type: "int", nullable: false),
                    ModelAutomobilaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Automobils", x => x.AutomobilId);
                    table.ForeignKey(
                        name: "FK_Automobils_Izdavacs_IzdavacId",
                        column: x => x.IzdavacId,
                        principalTable: "Izdavacs",
                        principalColumn: "IzdavacId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Automobils_ModelAutomobilas_ModelAutomobilaId",
                        column: x => x.ModelAutomobilaId,
                        principalTable: "ModelAutomobilas",
                        principalColumn: "ModelAutomobilaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Automobils_TipAutomobilas_TipAutomobilaId",
                        column: x => x.TipAutomobilaId,
                        principalTable: "TipAutomobilas",
                        principalColumn: "TipAutomobilaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Automobils_TipGorivas_TipGorivaId",
                        column: x => x.TipGorivaId,
                        principalTable: "TipGorivas",
                        principalColumn: "TipGorivaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Komentars",
                columns: table => new
                {
                    KomentarId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatumKomentara = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KorisnikId = table.Column<int>(type: "int", nullable: false),
                    AutomobilId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Komentars", x => x.KomentarId);
                    table.ForeignKey(
                        name: "FK_Komentars_Automobils_AutomobilId",
                        column: x => x.AutomobilId,
                        principalTable: "Automobils",
                        principalColumn: "AutomobilId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Komentars_Korisniks_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisniks",
                        principalColumn: "KorisnikId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ocjenas",
                columns: table => new
                {
                    OcjenaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrojOcjene = table.Column<int>(type: "int", nullable: false),
                    KorisnikId = table.Column<int>(type: "int", nullable: false),
                    AutomobilId = table.Column<int>(type: "int", nullable: false),
                    DatumOcjene = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ocjenas", x => x.OcjenaId);
                    table.ForeignKey(
                        name: "FK_Ocjenas_Automobils_AutomobilId",
                        column: x => x.AutomobilId,
                        principalTable: "Automobils",
                        principalColumn: "AutomobilId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ocjenas_Korisniks_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisniks",
                        principalColumn: "KorisnikId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rezervisanjes",
                columns: table => new
                {
                    RezervisanjeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumPocetka = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumZavrsetka = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KorisnikId = table.Column<int>(type: "int", nullable: false),
                    AutomobilId = table.Column<int>(type: "int", nullable: false),
                    StatusOcjene = table.Column<bool>(type: "bit", nullable: true),
                    StatusKomentara = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervisanjes", x => x.RezervisanjeId);
                    table.ForeignKey(
                        name: "FK_Rezervisanjes_Automobils_AutomobilId",
                        column: x => x.AutomobilId,
                        principalTable: "Automobils",
                        principalColumn: "AutomobilId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rezervisanjes_Korisniks_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisniks",
                        principalColumn: "KorisnikId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Automobils_IzdavacId",
                table: "Automobils",
                column: "IzdavacId");

            migrationBuilder.CreateIndex(
                name: "IX_Automobils_ModelAutomobilaId",
                table: "Automobils",
                column: "ModelAutomobilaId");

            migrationBuilder.CreateIndex(
                name: "IX_Automobils_TipAutomobilaId",
                table: "Automobils",
                column: "TipAutomobilaId");

            migrationBuilder.CreateIndex(
                name: "IX_Automobils_TipGorivaId",
                table: "Automobils",
                column: "TipGorivaId");

            migrationBuilder.CreateIndex(
                name: "IX_Izdavacs_GradId",
                table: "Izdavacs",
                column: "GradId");

            migrationBuilder.CreateIndex(
                name: "IX_Komentars_AutomobilId",
                table: "Komentars",
                column: "AutomobilId");

            migrationBuilder.CreateIndex(
                name: "IX_Komentars_KorisnikId",
                table: "Komentars",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Korisniks_GradId",
                table: "Korisniks",
                column: "GradId");

            migrationBuilder.CreateIndex(
                name: "IX_Korisniks_TipKorisnikaId",
                table: "Korisniks",
                column: "TipKorisnikaId");

            migrationBuilder.CreateIndex(
                name: "IX_ModelAutomobilas_ProizvodjacId",
                table: "ModelAutomobilas",
                column: "ProizvodjacId");

            migrationBuilder.CreateIndex(
                name: "IX_Ocjenas_AutomobilId",
                table: "Ocjenas",
                column: "AutomobilId");

            migrationBuilder.CreateIndex(
                name: "IX_Ocjenas_KorisnikId",
                table: "Ocjenas",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervisanjes_AutomobilId",
                table: "Rezervisanjes",
                column: "AutomobilId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervisanjes_KorisnikId",
                table: "Rezervisanjes",
                column: "KorisnikId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Komentars");

            migrationBuilder.DropTable(
                name: "Ocjenas");

            migrationBuilder.DropTable(
                name: "Rezervisanjes");

            migrationBuilder.DropTable(
                name: "Automobils");

            migrationBuilder.DropTable(
                name: "Korisniks");

            migrationBuilder.DropTable(
                name: "Izdavacs");

            migrationBuilder.DropTable(
                name: "ModelAutomobilas");

            migrationBuilder.DropTable(
                name: "TipAutomobilas");

            migrationBuilder.DropTable(
                name: "TipGorivas");

            migrationBuilder.DropTable(
                name: "TipKorisnikas");

            migrationBuilder.DropTable(
                name: "Grads");

            migrationBuilder.DropTable(
                name: "Proizvodjacs");
        }
    }
}
