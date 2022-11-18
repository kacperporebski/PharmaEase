using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmaEase.Migrations
{
    public partial class DoctorAndPatient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    MedicalLicenseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApprovAdminId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.MedicalLicenseId);
                });

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    GovtHealthNum = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuildingNum = table.Column<int>(type: "int", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.GovtHealthNum);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_PatientHealthNum",
                table: "Prescription",
                column: "PatientHealthNum");

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_PrescriberLicenseNum",
                table: "Prescription",
                column: "PrescriberLicenseNum");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Doctor_PrescriberLicenseNum",
                table: "Prescription");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Patient_PatientHealthNum",
                table: "Prescription");

            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropTable(
                name: "Patient");

            migrationBuilder.DropIndex(
                name: "IX_Prescription_PatientHealthNum",
                table: "Prescription");

            migrationBuilder.DropIndex(
                name: "IX_Prescription_PrescriberLicenseNum",
                table: "Prescription");
        }
    }
}
