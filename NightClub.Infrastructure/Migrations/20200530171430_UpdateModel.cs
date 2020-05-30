using Microsoft.EntityFrameworkCore.Migrations;

namespace NightClub.Infrastructure.Migrations
{
    public partial class UpdateModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blacklists_Members_MemberId1",
                table: "Blacklists");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberCards_Members_MemberId1",
                table: "MemberCards");

            migrationBuilder.DropIndex(
                name: "IX_MemberCards_MemberId1",
                table: "MemberCards");

            migrationBuilder.DropIndex(
                name: "IX_Blacklists_MemberId1",
                table: "Blacklists");

            migrationBuilder.DropColumn(
                name: "MemberId1",
                table: "MemberCards");

            migrationBuilder.DropColumn(
                name: "MemberId1",
                table: "Blacklists");

            migrationBuilder.UpdateData(
                table: "MemberCards",
                keyColumn: "Id",
                keyValue: 1,
                column: "Code",
                value: "2f704a64-3be8-48f3-91ac-128450500adf");

            migrationBuilder.UpdateData(
                table: "MemberCards",
                keyColumn: "Id",
                keyValue: 2,
                column: "Code",
                value: "19d8e8fa-58f1-42c9-9f3c-2661ce6943b0");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MemberId1",
                table: "MemberCards",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MemberId1",
                table: "Blacklists",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "MemberCards",
                keyColumn: "Id",
                keyValue: 1,
                column: "Code",
                value: "cb656a56-e038-448e-a509-541c549adf0c");

            migrationBuilder.UpdateData(
                table: "MemberCards",
                keyColumn: "Id",
                keyValue: 2,
                column: "Code",
                value: "3da36c94-ee93-4736-b152-b4c8f7cd8006");

            migrationBuilder.CreateIndex(
                name: "IX_MemberCards_MemberId1",
                table: "MemberCards",
                column: "MemberId1");

            migrationBuilder.CreateIndex(
                name: "IX_Blacklists_MemberId1",
                table: "Blacklists",
                column: "MemberId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Blacklists_Members_MemberId1",
                table: "Blacklists",
                column: "MemberId1",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberCards_Members_MemberId1",
                table: "MemberCards",
                column: "MemberId1",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
