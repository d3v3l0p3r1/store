using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class product_file_id : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Files_FileID",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "FileID",
                table: "Products",
                newName: "FileId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_FileID",
                table: "Products",
                newName: "IX_Products_FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Files_FileId",
                table: "Products",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Files_FileId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "FileId",
                table: "Products",
                newName: "FileID");

            migrationBuilder.RenameIndex(
                name: "IX_Products_FileId",
                table: "Products",
                newName: "IX_Products_FileID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Files_FileID",
                table: "Products",
                column: "FileID",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
