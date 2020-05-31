using Microsoft.EntityFrameworkCore.Migrations;

namespace NightClub.Infrastructure.Migrations
{
    public partial class AddUniqueConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MemberCards",
                keyColumn: "Id",
                keyValue: 1,
                column: "Code",
                value: "007c7060-2317-4e3c-85b2-cb1d78d3016e");

            migrationBuilder.UpdateData(
                table: "MemberCards",
                keyColumn: "Id",
                keyValue: 2,
                column: "Code",
                value: "e8aa251f-39a7-4db8-be3e-3e3a3f305529");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
