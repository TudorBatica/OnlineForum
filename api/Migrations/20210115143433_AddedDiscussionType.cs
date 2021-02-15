using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Forum.Migrations
{
    public partial class AddedDiscussionType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DiscussionDateTime",
                table: "Discussions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DiscussionTypeId",
                table: "Discussions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DiscussionReplyDateTime",
                table: "DiscussionReply",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "DiscussionType",
                columns: table => new
                {
                    DiscussionTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscussionType", x => x.DiscussionTypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Discussions_DiscussionTypeId",
                table: "Discussions",
                column: "DiscussionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Discussions_DiscussionType_DiscussionTypeId",
                table: "Discussions",
                column: "DiscussionTypeId",
                principalTable: "DiscussionType",
                principalColumn: "DiscussionTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discussions_DiscussionType_DiscussionTypeId",
                table: "Discussions");

            migrationBuilder.DropTable(
                name: "DiscussionType");

            migrationBuilder.DropIndex(
                name: "IX_Discussions_DiscussionTypeId",
                table: "Discussions");

            migrationBuilder.DropColumn(
                name: "DiscussionDateTime",
                table: "Discussions");

            migrationBuilder.DropColumn(
                name: "DiscussionTypeId",
                table: "Discussions");

            migrationBuilder.DropColumn(
                name: "DiscussionReplyDateTime",
                table: "DiscussionReply");
        }
    }
}
