using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmaEase.Migrations
{
    public partial class DoctorLinkedToUser3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Doctor_PrescriberLicenseNum",
                table: "Prescription");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Patient_PatientHealthNum",
                table: "Prescription");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Doctor",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_UserId",
                table: "Doctor",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctor_AspNetUsers_UserId",
                table: "Doctor",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Doctor_PrescriberLicenseNum",
                table: "Prescription",
                column: "PrescriberLicenseNum",
                principalTable: "Doctor",
                principalColumn: "MedicalLicenseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Patient_PatientHealthNum",
                table: "Prescription",
                column: "PatientHealthNum",
                principalTable: "Patient",
                principalColumn: "GovtHealthNum");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctor_AspNetUsers_UserId",
                table: "Doctor");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Doctor_PrescriberLicenseNum",
                table: "Prescription");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Patient_PatientHealthNum",
                table: "Prescription");

            migrationBuilder.DropIndex(
                name: "IX_Doctor_UserId",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Doctor");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Doctor_PrescriberLicenseNum",
                table: "Prescription",
                column: "PrescriberLicenseNum",
                principalTable: "Doctor",
                principalColumn: "MedicalLicenseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Patient_PatientHealthNum",
                table: "Prescription",
                column: "PatientHealthNum",
                principalTable: "Patient",
                principalColumn: "GovtHealthNum",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
