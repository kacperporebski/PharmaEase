using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmaEase.Migrations
{
    public partial class CommonNameForMedication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Medication_MedicationId",
                table: "Prescription");

            migrationBuilder.DropIndex(
                name: "IX_Prescription_MedicationId",
                table: "Prescription");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Medication",
                table: "Medication");

            migrationBuilder.DropColumn(
                name: "MedicationId",
                table: "Prescription");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Medication");

            migrationBuilder.AddColumn<string>(
                name: "MedicationCommonName",
                table: "Prescription",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CommonName",
                table: "Medication",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Medication",
                table: "Medication",
                column: "CommonName");

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_MedicationCommonName",
                table: "Prescription",
                column: "MedicationCommonName");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Medication_MedicationCommonName",
                table: "Prescription",
                column: "MedicationCommonName",
                principalTable: "Medication",
                principalColumn: "CommonName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Medication_MedicationCommonName",
                table: "Prescription");

            migrationBuilder.DropIndex(
                name: "IX_Prescription_MedicationCommonName",
                table: "Prescription");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Medication",
                table: "Medication");

            migrationBuilder.DropColumn(
                name: "MedicationCommonName",
                table: "Prescription");

            migrationBuilder.AddColumn<int>(
                name: "MedicationId",
                table: "Prescription",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "CommonName",
                table: "Medication",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Medication",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Medication",
                table: "Medication",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_MedicationId",
                table: "Prescription",
                column: "MedicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Medication_MedicationId",
                table: "Prescription",
                column: "MedicationId",
                principalTable: "Medication",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
