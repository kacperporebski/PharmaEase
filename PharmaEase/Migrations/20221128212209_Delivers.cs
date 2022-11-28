using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmaEase.Migrations
{
    public partial class Delivers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Delivers",
                columns: table => new
                {
                    PrescriptionID = table.Column<int>(type: "int", nullable: false),
                    CourierID = table.Column<int>(type: "int", nullable: false),
                    PharmacyID = table.Column<int>(type: "int", nullable: false),
                    PatientHealthNum = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Delivers", x => new { x.PrescriptionID, x.CourierID, x.PharmacyID, x.PatientHealthNum });
                    table.ForeignKey(
                        name: "FK_Delivers_Courier_CourierID",
                        column: x => x.CourierID,
                        principalTable: "Courier",
                        principalColumn: "CourierId");
                    table.ForeignKey(
                        name: "FK_Delivers_Patient_PatientHealthNum",
                        column: x => x.PatientHealthNum,
                        principalTable: "Patient",
                        principalColumn: "GovtHealthNum");
                    table.ForeignKey(
                        name: "FK_Delivers_Pharmacy_PharmacyID",
                        column: x => x.PharmacyID,
                        principalTable: "Pharmacy",
                        principalColumn: "PharmacyId");
                    table.ForeignKey(
                        name: "FK_Delivers_Prescription_PrescriptionID",
                        column: x => x.PrescriptionID,
                        principalTable: "Prescription",
                        principalColumn: "PrescriptionId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Delivers_CourierID",
                table: "Delivers",
                column: "CourierID");

            migrationBuilder.CreateIndex(
                name: "IX_Delivers_PatientHealthNum",
                table: "Delivers",
                column: "PatientHealthNum");

            migrationBuilder.CreateIndex(
                name: "IX_Delivers_PharmacyID",
                table: "Delivers",
                column: "PharmacyID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Delivers");
        }
    }
}
