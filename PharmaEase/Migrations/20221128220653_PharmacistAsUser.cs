using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmaEase.Migrations
{
    public partial class PharmacistAsUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovAdmindID",
                table: "Pharmacist");

            migrationBuilder.AddColumn<string>(
                name: "ApprovAdminID",
                table: "Pharmacist",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Pharmacist",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacist_UserId",
                table: "Pharmacist",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pharmacist_AspNetUsers_UserId",
                table: "Pharmacist",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pharmacist_AspNetUsers_UserId",
                table: "Pharmacist");

            migrationBuilder.DropIndex(
                name: "IX_Pharmacist_UserId",
                table: "Pharmacist");

            migrationBuilder.DropColumn(
                name: "ApprovAdminID",
                table: "Pharmacist");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Pharmacist");

            migrationBuilder.AddColumn<int>(
                name: "ApprovAdmindID",
                table: "Pharmacist",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
