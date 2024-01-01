using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hastane.Migrations
{
    public partial class doctorforeignkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Doctors_polic",
                table: "Doctors",
                column: "polic");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Policlinics_polic",
                table: "Doctors",
                column: "polic",
                principalTable: "Policlinics",
                principalColumn: "PolicId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Policlinics_polic",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_polic",
                table: "Doctors");
        }
    }
}
