using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackOfficeApp.Migrations
{
    /// <inheritdoc />
    public partial class AddEmprunts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emprunts_Livres_LivreISBN",
                table: "Emprunts");

            migrationBuilder.DropIndex(
                name: "IX_Emprunts_LivreISBN",
                table: "Emprunts");

            migrationBuilder.DropColumn(
                name: "LivreISBN",
                table: "Emprunts");

            migrationBuilder.RenameColumn(
                name: "DateRetour",
                table: "Emprunts",
                newName: "DateRetourPrevu");

            migrationBuilder.AlterColumn<string>(
                name: "ISBN",
                table: "Emprunts",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateRetourReel",
                table: "Emprunts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ReservationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdherentID = table.Column<int>(type: "int", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateReservation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatePrevuRetrait = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ReservationID);
                    table.ForeignKey(
                        name: "FK_Reservations_Adherents_AdherentID",
                        column: x => x.AdherentID,
                        principalTable: "Adherents",
                        principalColumn: "AdherentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Livres_ISBN",
                        column: x => x.ISBN,
                        principalTable: "Livres",
                        principalColumn: "ISBN",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Emprunts_ISBN",
                table: "Emprunts",
                column: "ISBN");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_AdherentID",
                table: "Reservations",
                column: "AdherentID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ISBN",
                table: "Reservations",
                column: "ISBN");

            migrationBuilder.AddForeignKey(
                name: "FK_Emprunts_Livres_ISBN",
                table: "Emprunts",
                column: "ISBN",
                principalTable: "Livres",
                principalColumn: "ISBN",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emprunts_Livres_ISBN",
                table: "Emprunts");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Emprunts_ISBN",
                table: "Emprunts");

            migrationBuilder.DropColumn(
                name: "DateRetourReel",
                table: "Emprunts");

            migrationBuilder.RenameColumn(
                name: "DateRetourPrevu",
                table: "Emprunts",
                newName: "DateRetour");

            migrationBuilder.AlterColumn<string>(
                name: "ISBN",
                table: "Emprunts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "LivreISBN",
                table: "Emprunts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Emprunts_LivreISBN",
                table: "Emprunts",
                column: "LivreISBN");

            migrationBuilder.AddForeignKey(
                name: "FK_Emprunts_Livres_LivreISBN",
                table: "Emprunts",
                column: "LivreISBN",
                principalTable: "Livres",
                principalColumn: "ISBN",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
