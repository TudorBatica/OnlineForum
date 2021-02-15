using Microsoft.EntityFrameworkCore.Migrations;

namespace Forum.Migrations
{
    public partial class SingleCareer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CareerDiscussion");

            migrationBuilder.AddColumn<int>(
                name: "CareerId",
                table: "Discussions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Discussions_CareerId",
                table: "Discussions",
                column: "CareerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Discussions_Careers_CareerId",
                table: "Discussions",
                column: "CareerId",
                principalTable: "Careers",
                principalColumn: "CareerId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discussions_Careers_CareerId",
                table: "Discussions");

            migrationBuilder.DropIndex(
                name: "IX_Discussions_CareerId",
                table: "Discussions");

            migrationBuilder.DropColumn(
                name: "CareerId",
                table: "Discussions");

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
        }
    }
}
