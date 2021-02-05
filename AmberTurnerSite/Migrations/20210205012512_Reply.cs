using Microsoft.EntityFrameworkCore.Migrations;

namespace AmberTurnerSite.Migrations
{
    public partial class Reply : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_PostCreatorId",
                table: "Posts");

            migrationBuilder.AlterColumn<string>(
                name: "PostCreatorId",
                table: "Posts",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "Replies",
                columns: table => new
                {
                    ReplyID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ForumID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Replies", x => x.ReplyID);
                    table.ForeignKey(
                        name: "FK_Replies_Posts_ForumID",
                        column: x => x.ForumID,
                        principalTable: "Posts",
                        principalColumn: "ForumID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Replies_ForumID",
                table: "Replies",
                column: "ForumID");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_PostCreatorId",
                table: "Posts",
                column: "PostCreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_PostCreatorId",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "Replies");

            migrationBuilder.AlterColumn<string>(
                name: "PostCreatorId",
                table: "Posts",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_PostCreatorId",
                table: "Posts",
                column: "PostCreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
