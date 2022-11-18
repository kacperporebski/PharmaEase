using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmaEase.Migrations
{
    public partial class CommonNameForMedication2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Medication_MedicationCommonName",
                table: "Prescription");

            migrationBuilder.AlterColumn<string>(
                name: "MedicationCommonName",
                table: "Prescription",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Medication_MedicationCommonName",
                table: "Prescription",
                column: "MedicationCommonName",
                principalTable: "Medication",
                principalColumn: "CommonName",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Medication_MedicationCommonName",
                table: "Prescription");

            migrationBuilder.AlterColumn<string>(
                name: "MedicationCommonName",
                table: "Prescription",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Medication_MedicationCommonName",
                table: "Prescription",
                column: "MedicationCommonName",
                principalTable: "Medication",
                principalColumn: "CommonName");
        }
    }
}
