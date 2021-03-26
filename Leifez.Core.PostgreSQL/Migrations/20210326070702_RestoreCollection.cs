using Microsoft.EntityFrameworkCore.Migrations;

namespace Leifez.Core.PostgreSQL.Migrations
{
    public partial class RestoreCollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Collections",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DbCollectionDbImage",
                columns: table => new
                {
                    CollectionsId = table.Column<int>(type: "integer", nullable: false),
                    ImagesGuid = table.Column<string>(type: "character varying(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbCollectionDbImage", x => new { x.CollectionsId, x.ImagesGuid });
                    table.ForeignKey(
                        name: "FK_DbCollectionDbImage_Collections_CollectionsId",
                        column: x => x.CollectionsId,
                        principalTable: "Collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DbCollectionDbImage_Images_ImagesGuid",
                        column: x => x.ImagesGuid,
                        principalTable: "Images",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Collections_AuthorId",
                table: "Collections",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_DbCollectionDbImage_ImagesGuid",
                table: "DbCollectionDbImage",
                column: "ImagesGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_AspNetUsers_AuthorId",
                table: "Collections",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collections_AspNetUsers_AuthorId",
                table: "Collections");

            migrationBuilder.DropTable(
                name: "DbCollectionDbImage");

            migrationBuilder.DropIndex(
                name: "IX_Collections_AuthorId",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Collections");
        }
    }
}
