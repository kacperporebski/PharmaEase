using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmaEase.Migrations
{
    public partial class DoctorLinkedToUser2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Doctor_PrescriberLicenseNum",
                table: "Prescription");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Doctor_PrescriberLicenseNum",
                table: "Prescription",
                column: "PrescriberLicenseNum",
                principalTable: "Doctor",
                principalColumn: "MedicalLicenseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Doctor_PrescriberLicenseNum",
                table: "Prescription");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Doctor_PrescriberLicenseNum",
                table: "Prescription",
                column: "PrescriberLicenseNum",
                principalTable: "Doctor",
                principalColumn: "MedicalLicenseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
