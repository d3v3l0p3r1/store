using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WebApi.Migrations
{
    public partial class editcarousel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarouselItem");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Carousels",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FileId",
                table: "Carousels",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Header",
                table: "Carousels",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Href",
                table: "Carousels",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Carousels",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProductId1",
                table: "Carousels",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Show",
                table: "Carousels",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Carousels_FileId",
                table: "Carousels",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_Carousels_ProductId1",
                table: "Carousels",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Carousels_Files_FileId",
                table: "Carousels",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Carousels_Products_ProductId1",
                table: "Carousels",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carousels_Files_FileId",
                table: "Carousels");

            migrationBuilder.DropForeignKey(
                name: "FK_Carousels_Products_ProductId1",
                table: "Carousels");

            migrationBuilder.DropIndex(
                name: "IX_Carousels_FileId",
                table: "Carousels");

            migrationBuilder.DropIndex(
                name: "IX_Carousels_ProductId1",
                table: "Carousels");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Carousels");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "Carousels");

            migrationBuilder.DropColumn(
                name: "Header",
                table: "Carousels");

            migrationBuilder.DropColumn(
                name: "Href",
                table: "Carousels");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Carousels");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "Carousels");

            migrationBuilder.DropColumn(
                name: "Show",
                table: "Carousels");

            migrationBuilder.CreateTable(
                name: "CarouselItem",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CarouselId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    FileId = table.Column<long>(type: "bigint", nullable: false),
                    Header = table.Column<string>(type: "text", nullable: true),
                    Hidden = table.Column<bool>(type: "boolean", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    ProductId1 = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarouselItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarouselItem_Carousels_CarouselId",
                        column: x => x.CarouselId,
                        principalTable: "Carousels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarouselItem_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarouselItem_Products_ProductId1",
                        column: x => x.ProductId1,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarouselItem_CarouselId",
                table: "CarouselItem",
                column: "CarouselId");

            migrationBuilder.CreateIndex(
                name: "IX_CarouselItem_FileId",
                table: "CarouselItem",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_CarouselItem_ProductId1",
                table: "CarouselItem",
                column: "ProductId1");
        }
    }
}
