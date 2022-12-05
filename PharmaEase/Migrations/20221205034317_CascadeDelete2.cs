using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmaEase.Migrations
{
    public partial class CascadeDelete2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Delivers_Courier_CourierID",
                table: "Delivers");

            migrationBuilder.DropForeignKey(
                name: "FK_Delivers_Patient_PatientHealthNum",
                table: "Delivers");

            migrationBuilder.DropForeignKey(
                name: "FK_Delivers_Pharmacy_PharmacyID",
                table: "Delivers");

            migrationBuilder.AddForeignKey(
                name: "FK_Delivers_Courier_CourierID",
                table: "Delivers",
                column: "CourierID",
                principalTable: "Courier",
                principalColumn: "CourierId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Delivers_Patient_PatientHealthNum",
                table: "Delivers",
                column: "PatientHealthNum",
                principalTable: "Patient",
                principalColumn: "GovtHealthNum",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Delivers_Pharmacy_PharmacyID",
                table: "Delivers",
                column: "PharmacyID",
                principalTable: "Pharmacy",
                principalColumn: "PharmacyId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Delivers_Courier_CourierID",
                table: "Delivers");

            migrationBuilder.DropForeignKey(
                name: "FK_Delivers_Patient_PatientHealthNum",
                table: "Delivers");

            migrationBuilder.DropForeignKey(
                name: "FK_Delivers_Pharmacy_PharmacyID",
                table: "Delivers");

            migrationBuilder.AddForeignKey(
                name: "FK_Delivers_Courier_CourierID",
                table: "Delivers",
                column: "CourierID",
                principalTable: "Courier",
                principalColumn: "CourierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Delivers_Patient_PatientHealthNum",
                table: "Delivers",
                column: "PatientHealthNum",
                principalTable: "Patient",
                principalColumn: "GovtHealthNum");

            migrationBuilder.AddForeignKey(
                name: "FK_Delivers_Pharmacy_PharmacyID",
                table: "Delivers",
                column: "PharmacyID",
                principalTable: "Pharmacy",
                principalColumn: "PharmacyId");
        }
    }
}
