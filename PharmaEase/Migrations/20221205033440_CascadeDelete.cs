using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmaEase.Migrations
{
    public partial class CascadeDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Delivers_Prescription_PrescriptionID",
                table: "Delivers");

            migrationBuilder.AddForeignKey(
                name: "FK_Delivers_Prescription_PrescriptionID",
                table: "Delivers",
                column: "PrescriptionID",
                principalTable: "Prescription",
                principalColumn: "PrescriptionId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Delivers_Prescription_PrescriptionID",
                table: "Delivers");

            migrationBuilder.AddForeignKey(
                name: "FK_Delivers_Prescription_PrescriptionID",
                table: "Delivers",
                column: "PrescriptionID",
                principalTable: "Prescription",
                principalColumn: "PrescriptionId");
        }
    }
}
