using Microsoft.EntityFrameworkCore.Migrations;

namespace NightClub.Infrastructure.Migrations
{
    public partial class RemoveNissConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MemberCards",
                keyColumn: "Id",
                keyValue: 1,
                column: "Code",
                value: "bff52a3b-e465-43fd-a551-f845b99a2a43");

            migrationBuilder.UpdateData(
                table: "MemberCards",
                keyColumn: "Id",
                keyValue: 2,
                column: "Code",
                value: "f6275549-132f-4b9c-a87f-ac5ada040eae");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
