using Microsoft.EntityFrameworkCore.Migrations;

namespace Forum.Migrations
{
    public partial class Careers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiscussionReply_Discussions_DiscussionId",
                table: "DiscussionReply");

            migrationBuilder.DropForeignKey(
                name: "FK_Discussions_DiscussionType_DiscussionTypeId",
                table: "Discussions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DiscussionType",
                table: "DiscussionType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DiscussionReply",
                table: "DiscussionReply");

            migrationBuilder.RenameTable(
                name: "DiscussionType",
                newName: "DiscussionTypes");

            migrationBuilder.RenameTable(
                name: "DiscussionReply",
                newName: "DiscussionReplies");

            migrationBuilder.RenameColumn(
                name: "Score",
                table: "Discussions",
                newName: "NoOfViews");

            migrationBuilder.RenameIndex(
                name: "IX_DiscussionReply_DiscussionId",
                table: "DiscussionReplies",
                newName: "IX_DiscussionReplies_DiscussionId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "DiscussionReplies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiscussionTypes",
                table: "DiscussionTypes",
                column: "DiscussionTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiscussionReplies",
                table: "DiscussionReplies",
                column: "DiscussionReplyId");

            migrationBuilder.CreateTable(
                name: "Careers",
                columns: table => new
                {
                    CareerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Careers", x => x.CareerId);
                });

            migrationBuilder.CreateTable(
                name: "CareerDiscussion",
                columns: table => new
                {
                    CareersCareerId = table.Column<int>(type: "int", nullable: false),
                    DiscussionsDiscussionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CareerDiscussion", x => new { x.CareersCareerId, x.DiscussionsDiscussionId });
                    table.ForeignKey(
                        name: "FK_CareerDiscussion_Careers_CareersCareerId",
                        column: x => x.CareersCareerId,
                        principalTable: "Careers",
                        principalColumn: "CareerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CareerDiscussion_Discussions_DiscussionsDiscussionId",
                        column: x => x.DiscussionsDiscussionId,
                        principalTable: "Discussions",
                        principalColumn: "DiscussionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CareerDiscussion_DiscussionsDiscussionId",
                table: "CareerDiscussion",
                column: "DiscussionsDiscussionId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiscussionReplies_Discussions_DiscussionId",
                table: "DiscussionReplies",
                column: "DiscussionId",
                principalTable: "Discussions",
                principalColumn: "DiscussionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Discussions_DiscussionTypes_DiscussionTypeId",
                table: "Discussions",
                column: "DiscussionTypeId",
                principalTable: "DiscussionTypes",
                principalColumn: "DiscussionTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiscussionReplies_Discussions_DiscussionId",
                table: "DiscussionReplies");

            migrationBuilder.DropForeignKey(
                name: "FK_Discussions_DiscussionTypes_DiscussionTypeId",
                table: "Discussions");

            migrationBuilder.DropTable(
                name: "CareerDiscussion");

            migrationBuilder.DropTable(
                name: "Careers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DiscussionTypes",
                table: "DiscussionTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DiscussionReplies",
                table: "DiscussionReplies");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "DiscussionReplies");

            migrationBuilder.RenameTable(
                name: "DiscussionTypes",
                newName: "DiscussionType");

            migrationBuilder.RenameTable(
                name: "DiscussionReplies",
                newName: "DiscussionReply");

            migrationBuilder.RenameColumn(
                name: "NoOfViews",
                table: "Discussions",
                newName: "Score");

            migrationBuilder.RenameIndex(
                name: "IX_DiscussionReplies_DiscussionId",
                table: "DiscussionReply",
                newName: "IX_DiscussionReply_DiscussionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiscussionType",
                table: "DiscussionType",
                column: "DiscussionTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiscussionReply",
                table: "DiscussionReply",
                column: "DiscussionReplyId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiscussionReply_Discussions_DiscussionId",
                table: "DiscussionReply",
                column: "DiscussionId",
                principalTable: "Discussions",
                principalColumn: "DiscussionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Discussions_DiscussionType_DiscussionTypeId",
                table: "Discussions",
                column: "DiscussionTypeId",
                principalTable: "DiscussionType",
                principalColumn: "DiscussionTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
