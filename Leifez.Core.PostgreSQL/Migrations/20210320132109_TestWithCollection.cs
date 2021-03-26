using Microsoft.EntityFrameworkCore.Migrations;

namespace Leifez.Core.PostgreSQL.Migrations
{
    public partial class TestWithCollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Collections_DbCollectionId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Collections_DbCollectionId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_DbCollectionId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Images_DbCollectionId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "DbCollectionId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "DbCollectionId",
                table: "Images");

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

            migrationBuilder.CreateTable(
                name: "DbCollectionDbTag",
                columns: table => new
                {
                    CollectionsId = table.Column<int>(type: "integer", nullable: false),
                    TagsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbCollectionDbTag", x => new { x.CollectionsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_DbCollectionDbTag_Collections_CollectionsId",
                        column: x => x.CollectionsId,
                        principalTable: "Collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DbCollectionDbTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DbCollectionDbImage_ImagesGuid",
                table: "DbCollectionDbImage",
                column: "ImagesGuid");

            migrationBuilder.CreateIndex(
                name: "IX_DbCollectionDbTag_TagsId",
                table: "DbCollectionDbTag",
                column: "TagsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DbCollectionDbImage");

            migrationBuilder.DropTable(
                name: "DbCollectionDbTag");

            migrationBuilder.AddColumn<int>(
                name: "DbCollectionId",
                table: "Tags",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DbCollectionId",
                table: "Images",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_DbCollectionId",
                table: "Tags",
                column: "DbCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_DbCollectionId",
                table: "Images",
                column: "DbCollectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Collections_DbCollectionId",
                table: "Images",
                column: "DbCollectionId",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Collections_DbCollectionId",
                table: "Tags",
                column: "DbCollectionId",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
